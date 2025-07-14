using UnityEngine;

public class BattlefieldSpawnController : MonoBehaviour
{
    private UIDeployController uIDeployController;
    private Transform ghostParent;
    private CreatureCatalog creatureCatalog;
    private GhostCreatureHandler ghostHandler;
    private CreatureStack selectedStack;

    public BattlefieldSpawnController(CreatureCatalog catalog, Transform ghostParentTransform, UIDeployController newUIDeployController)
    {
        creatureCatalog = catalog;
        ghostParent = ghostParentTransform;
        ghostHandler = new GhostCreatureHandler(ghostParent, catalog);
        uIDeployController = newUIDeployController;
        uIDeployController.OnSlotSelected += SetSelectedCreature;
    }

    private void OnDestroy()
    {
        uIDeployController.OnSlotSelected -= SetSelectedCreature;

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
}
