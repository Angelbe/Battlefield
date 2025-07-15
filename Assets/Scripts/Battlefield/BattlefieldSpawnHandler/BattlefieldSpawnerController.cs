using UnityEngine;

public class BattlefieldSpawnController : MonoBehaviour
{
    [SerializeField]
    private Transform ghostParent;
    private BattlefieldMouseHandler bfMouseHandler;
    private UIDeployController uIDeployController;
    private CreatureCatalog creatureCatalog;
    private GhostCreatureHandler ghostHandler;
    private CreatureStack selectedStack;



    private void OnDestroy()
    {
        uIDeployController.OnSlotSelected -= SetSelectedCreature;
        bfMouseHandler.OnTileHovered -= UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered += ClearGhost;
    }

    public void SetSelectedCreature(CreatureStack creature)
    {
        selectedStack = creature;
    }

    public void UpdateHoverTile(TileController tileController)
    {
        if (selectedStack == null) return;

        ghostHandler.ShowGhost(selectedStack, tileController);
    }

    public void ClearGhost()
    {
        ghostHandler.HideGhost();
    }

    public void TrySpawnCreatureAt(CubeCoord coord)
    {
        // Aquí más adelante iría la lógica real de deploy
        Debug.Log($"[BattlefieldSpawnHandler] Intentando desplegar {selectedStack.Creature.Name} en {coord}");
    }

    public void Init(CreatureCatalog newCreatureCatalog, UIDeployController newUIDeployController, BattlefieldMouseHandler newBFMouseHandler)
    {
        creatureCatalog = newCreatureCatalog;
        ghostHandler = new GhostCreatureHandler(ghostParent, creatureCatalog);
        uIDeployController = newUIDeployController;
        bfMouseHandler = newBFMouseHandler;
        uIDeployController.OnSlotSelected += SetSelectedCreature;
        bfMouseHandler.OnTileHovered += UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered += ClearGhost;
    }
}
