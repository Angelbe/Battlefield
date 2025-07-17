using UnityEngine;

public class BattlefieldSpawnController : MonoBehaviour, IBattlefieldSpawnController
{
    [SerializeField] private GameObject ghostUnitsGO;
    [SerializeField] private GameObject attackerUnitsGO;
    [SerializeField] private GameObject defenderUnitsGO;
    private BattlefieldController bfController;
    private BattlefieldMouseHandler bfMouseHandler;
    public UIDeployController uIDeployController;
    private CreatureCatalog creatureCatalog;
    private GhostCreatureHandler ghostHandler;
    private CreatureShapeCatalog shapeCatalog;
    public bool isShowingGhosts { get; set; } = false;
    private CreatureStack selectedStack;
    private DeploySlotController slotSelected;
    private TileController tileHovered;
    private Army activeArmy;
    private Color selectedColor => bfController.BfConfig.GetColor(ETileHighlightType.Selected);
    private Color hoverColor => bfController.BfConfig.GetColor(ETileHighlightType.Hover);
    private Color deployColor => bfController.BfConfig.GetColor(ETileHighlightType.DeployZone);
    private CreaturePlacementValidator placementValidator;

    public void HandleSlotClicked(DeploySlotController slotClicked)
    {
        if (slotSelected == null)
        {
            StackSelectedToDeploy(slotClicked);
            return;
        }
        if (slotSelected.Model.CreatureStack.ID == slotClicked.Model.CreatureStack.ID)
        {
            StackUnselected();
            return;
        }
        StackUnselected();
        StackSelectedToDeploy(slotClicked);
    }

    private void StackDeployed(CreatureController creatureDeployed)
    {
        slotSelected.UnselectSlot();
        slotSelected.SlotDeployed();
        activeArmy.Deployed.AddStackToDeploy(slotSelected.Model.CreatureStack, creatureDeployed);
        slotSelected = null;
        selectedStack = null;
        StopShowingGhosts();
    }


    private void StackSelectedToDeploy(DeploySlotController newDeploySlotSelected)
    {
        slotSelected = newDeploySlotSelected;
        slotSelected.SlotSelected();
        selectedStack = slotSelected.Model.CreatureStack;
        ShowGhosts();
    }

    private void StackUnselected()
    {
        slotSelected.UnselectSlot();
        slotSelected = null;
        selectedStack = null;
        StopShowingGhosts();
    }

    private void UpdateHoverTile(TileController newTileHovered)
    {
        if (tileHovered != null)
            tileHovered.Highlight.RemoveColor(4, hoverColor);

        tileHovered = newTileHovered;

        if (isShowingGhosts && selectedStack != null)
        {
            if (placementValidator.DoesTheCreatureFitInTile(selectedStack.Creature, newTileHovered.Model.Coord))
            {
                ghostHandler.ShowGhost(selectedStack, newTileHovered);
                tileHovered.Highlight.AddColor(4, hoverColor);
            }
            else
            {
                ghostHandler.HideGhost(); // por si acaso
            }
        }
    }


    private void ShowGhosts()
    {
        isShowingGhosts = true;
    }

    private void StopShowingGhosts()
    {
        isShowingGhosts = false;
    }

    private void ClearGhost()
    {
        if (tileHovered != null)
            tileHovered.Highlight.RemoveColor(4, hoverColor);

        ghostHandler.HideGhost();
    }


    public void SetActiveArmy(Army army)
    {
        activeArmy = army;
    }


    public void HandleTileClicked(TileController tileClicked)
    {
        if (!isShowingGhosts || tileClicked != null)
        {
            if (selectedStack == null || !placementValidator.DoesTheCreatureFitInTile(selectedStack.Creature, tileClicked.Model.Coord))
            {
                return;
            }
        }
        bool isDefender = activeArmy.IsDefender;
        CreatureModel creatureModel = selectedStack.Creature;
        TileModel tileModel = tileClicked.Model;
        Transform ArmyTransform = isDefender ? attackerUnitsGO.transform : defenderUnitsGO.transform;
        GameObject CreaturePrefab = creatureCatalog.GetCombatPrefab(creatureModel.Name);
        GameObject CreatureGO = Instantiate(CreaturePrefab, ArmyTransform);
        CreatureController creatureController = CreatureGO.GetComponent<CreatureController>();
        CreatureGO.transform.position = tileModel.WorldPosition;
        creatureController.Init(creatureModel, tileModel.Coord, selectedStack.Quantity, isDefender);
        StackDeployed(creatureController);
        tileClicked.SetOcupantCreature(creatureController);
        tileClicked.Highlight.RemoveColor(5, selectedColor);
        tileClicked.Highlight.AddColor(2, activeArmy.ArmyColor);
    }

    public void Init(BattlefieldController newBfcontroller, CreatureCatalog newCreatureCatalog, UIDeployController newUIDeployController, BattlefieldMouseHandler newBFMouseHandler)
    {
        creatureCatalog = newCreatureCatalog;
        uIDeployController = newUIDeployController;
        bfMouseHandler = newBFMouseHandler;
        bfController = newBfcontroller;
        ghostHandler = new GhostCreatureHandler(ghostUnitsGO.transform, creatureCatalog);
        shapeCatalog = new CreatureShapeCatalog();
        placementValidator = new CreaturePlacementValidator(bfController, shapeCatalog, deployColor);
        uIDeployController.OnSlotClicked += HandleSlotClicked;
        bfMouseHandler.OnTileHovered += UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered += ClearGhost;
        bfMouseHandler.OnTileClicked += HandleTileClicked;
    }

    public void Shutdown()
    {
        uIDeployController.OnSlotClicked -= HandleSlotClicked;
        bfMouseHandler.OnTileHovered -= UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered -= ClearGhost;
        bfMouseHandler.OnTileClicked -= HandleTileClicked;
    }
}
