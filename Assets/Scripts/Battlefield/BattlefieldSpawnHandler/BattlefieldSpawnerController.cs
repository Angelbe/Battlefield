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

    private bool IsCreatureShapeCorrect(CreatureModel creatureToCheck, TileController tileAnchor)
    {
        if (selectedStack == null || tileHovered == null)
            return false;

        CubeCoord anchor = tileAnchor.Model.Coord;
        CubeCoord[] shapeOffsets = shapeCatalog.GetShape(creatureToCheck.Shape);

        foreach (var offset in shapeOffsets)
        {
            CubeCoord targetCoord = anchor + offset;

            // Si no existe en el diccionario de TileControllers, está fuera del mapa
            if (!bfMouseHandler.TileControllers.TryGetValue(targetCoord, out var tile))
            {
                // Debug.LogWarning($"[Spawn] Tile en {targetCoord} no existe.");
                return false;
            }

            // Si hay algo ocupando la tile
            if (tile.OccupantCreature != null)
            {
                // Debug.LogWarning($"[Spawn] Tile en {targetCoord} ya ocupada.");
                return false;
            }

            // Si no está dentro de una zona de despliegue válida (simplificable más adelante)
            if (tile.Highlight.originalHl != ETileHighlightType.DeployZone)
            {
                // Debug.LogWarning($"[Spawn] Tile en {targetCoord} fuera de zona de despliegue.");
                return false;
            }
        }

        return true;
    }

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

    private void StackSelectedToDeploy(DeploySlotController newDeploySlotSelected)
    {
        slotSelected = newDeploySlotSelected;
        slotSelected.SlotSelected();
        selectedStack = slotSelected.Model.CreatureStack;
        ShowGhosts();
    }

    private void StackDeployed()
    {
        slotSelected.UnselectSlot();
        slotSelected.SlotDeployed();
        slotSelected = null;
        selectedStack = null;
        StopShowingGhosts();
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
        ClearGhost();
        tileHovered = newTileHovered;

        if (isShowingGhosts && selectedStack != null)
        {
            if (IsCreatureShapeCorrect(selectedStack.Creature, newTileHovered))
            {
                ghostHandler.ShowGhost(selectedStack, newTileHovered);
            }
            else
            {
                // Debug.LogWarning($"[Spawn] No se puede colocar {selectedStack.Creature.Name} en {tileHovered.Model.Coord}");
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
        ghostHandler.HideGhost();
    }

    public void HandleTileClicked(TileController tileClicked)
    {
        if (!isShowingGhosts || tileClicked != null)
        {
            if (selectedStack == null || !IsCreatureShapeCorrect(selectedStack.Creature, tileClicked))
            {
                return;
            }
        }
        bool isAttacker = bfController.ActiveArmy.IsAttacker;
        Transform ArmyTransform = isAttacker ? attackerUnitsGO.transform : defenderUnitsGO.transform;
        GameObject CreaturePrefab = creatureCatalog.GetCombatPrefab(selectedStack.Creature.Name);
        GameObject CreatureGO = Instantiate(CreaturePrefab, ArmyTransform);
        CreatureController creaturecontroller = CreatureGO.GetComponent<CreatureController>();
        CreatureGO.transform.position = tileClicked.Model.WorldPosition;
        StackDeployed();
        tileClicked.SetOcupantCreature(creaturecontroller);
    }

    public void Init(BattlefieldController newBfcontroller, CreatureCatalog newCreatureCatalog, UIDeployController newUIDeployController, BattlefieldMouseHandler newBFMouseHandler)
    {
        creatureCatalog = newCreatureCatalog;
        uIDeployController = newUIDeployController;
        bfMouseHandler = newBFMouseHandler;
        bfController = newBfcontroller;
        ghostHandler = new GhostCreatureHandler(ghostUnitsGO.transform, creatureCatalog);
        shapeCatalog = new CreatureShapeCatalog();
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
