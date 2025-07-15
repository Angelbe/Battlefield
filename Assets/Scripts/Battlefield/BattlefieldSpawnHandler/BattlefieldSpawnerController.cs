using UnityEngine;

public class BattlefieldSpawnController : MonoBehaviour
{
    [SerializeField]
    private Transform ghostParent;
    private BattlefieldMouseHandler bfMouseHandler;
    private UIDeployController uIDeployController;
    private CreatureCatalog creatureCatalog;
    private GhostCreatureHandler ghostHandler;
    private CreatureShapeCatalog shapeCatalog;
    public bool isShowingGhosts = false;
    private CreatureStack selectedStack;
    private TileController tileHovered;



    private void OnDestroy()
    {
        uIDeployController.OnSlotSelected -= StackSelectedToDeploy;
        uIDeployController.OnSlotUnselected -= ClearStackSelectedToDeploy;
        bfMouseHandler.OnTileHovered -= UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered -= ClearGhost;
    }

    public bool IsCreatureShapeCorrect()
    {
        if (selectedStack == null || tileHovered == null)
            return false;

        CubeCoord anchor = tileHovered.Model.Coord;
        CubeCoord[] shapeOffsets = shapeCatalog.GetShape(selectedStack.Creature.Shape);

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
            if (tile.OccupantModel != null)
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

    public void StackSelectedToDeploy(CreatureStack creature)
    {
        selectedStack = creature;
        ShowGhosts();
    }

    public void ClearStackSelectedToDeploy()
    {
        selectedStack = null;
        StopShowingGhosts();
    }

    public void UpdateHoverTile(TileController newTileHovered)
    {
        ClearGhost();
        tileHovered = newTileHovered;

        if (isShowingGhosts && selectedStack != null)
        {
            if (IsCreatureShapeCorrect())
            {
                ghostHandler.ShowGhost(selectedStack, newTileHovered);
            }
            else
            {
                // Debug.LogWarning($"[Spawn] No se puede colocar {selectedStack.Creature.Name} en {tileHovered.Model.Coord}");
            }
        }
    }


    public void ShowGhosts()
    {
        isShowingGhosts = true;
    }

    public void StopShowingGhosts()
    {
        isShowingGhosts = false;
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
        uIDeployController.OnSlotSelected += StackSelectedToDeploy;
        uIDeployController.OnSlotUnselected += ClearStackSelectedToDeploy;
        bfMouseHandler.OnTileHovered += UpdateHoverTile;
        bfMouseHandler.OnTileUnhovered += ClearGhost;
        shapeCatalog = new CreatureShapeCatalog();
    }
}
