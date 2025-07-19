using UnityEngine;

public class BattlefieldSpawnController : MonoBehaviour, IBattlefieldSpawnController
{
    [SerializeField] private GameObject ghostUnitsGO;
    [SerializeField] private GameObject attackerUnitsGO;
    [SerializeField] private GameObject defenderUnitsGO;

    private BattlefieldController bfController;
    private BattlefieldMouseHandler bfMouseHandler;
    public UIDeployController uIDeployController;

    private DeploySelectionHandler selectionHandler;
    private SpawnValidator spawnValidator;
    private CreatureSpawner creatureSpawner;
    private GhostCreatureHandler ghostHandler;

    private TileController tileHovered;
    private Army activeArmy;

    private Color selectedColor => bfController.BfConfig.GetColor(ETileHighlightType.Selected);
    private Color hoverColor => bfController.BfConfig.GetColor(ETileHighlightType.Hover);

    public void Init(
        BattlefieldController newBfcontroller,
        CreatureCatalog newCreatureCatalog,
        UIDeployController newUIDeployController,
        BattlefieldMouseHandler newBFMouseHandler)
    {
        bfController = newBfcontroller;
        bfMouseHandler = newBFMouseHandler;
        uIDeployController = newUIDeployController;

        selectionHandler = new DeploySelectionHandler();
        spawnValidator = new SpawnValidator(bfController);
        creatureSpawner = new CreatureSpawner(
            bfController,
            newCreatureCatalog,
            attackerUnitsGO.transform,
            defenderUnitsGO.transform
        );
        ghostHandler = new GhostCreatureHandler(ghostUnitsGO.transform, newCreatureCatalog);

        uIDeployController.OnSlotClicked += selectionHandler.HandleSlotClicked;
        bfMouseHandler.OnTileHovered += UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered += ClearGhost;
        bfMouseHandler.OnTileClickedDeploymentPhase += HandleTileClicked;
    }

    public void SetActiveArmy(Army army)
    {
        activeArmy = army;
    }

    private void UpdateHoverTile(TileController newTileHovered)
    {
        tileHovered = newTileHovered;
        if (!selectionHandler.HasSelection) return;

        CreatureStack stack = selectionHandler.SelectedStack;
        if (spawnValidator.CanDeployStackInTile(stack, tileHovered))
        {
            ghostHandler.ShowGhost(stack, tileHovered);
        }
        else
        {
            ClearGhost();
        }
    }

    private void ClearGhost()
    {
        ghostHandler.HideGhost();
    }

    public void HandleTileClicked(TileController tileClicked)
    {
        if (!selectionHandler.HasSelection || tileClicked == null) return;
        CreatureStack stack = selectionHandler.SelectedStack;

        if (!spawnValidator.CanDeployStackInTile(stack, tileClicked)) return;
        CreatureController controller = creatureSpawner.SpawnCreature(stack, tileClicked, activeArmy);
        selectionHandler.ConfirmDeployment();
        ghostHandler.HideGhost();
        tileClicked.Highlight.RemoveColor(5, selectedColor);
    }

    public void Shutdown()
    {
        uIDeployController.OnSlotClicked -= selectionHandler.HandleSlotClicked;
        bfMouseHandler.OnTileHovered -= UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered -= ClearGhost;
        bfMouseHandler.OnTileClickedDeploymentPhase -= HandleTileClicked;
    }
}
