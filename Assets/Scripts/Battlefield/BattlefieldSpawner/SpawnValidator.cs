using System.Collections.Generic;
using UnityEngine;

public class SpawnValidator
{
    private readonly BattlefieldController bfController;
    private readonly CreatureShapeCatalog shapeCatalog = new();
    private Color deployColor => bfController.BfConfig.GetColor(ETileHighlightType.DeployZone);

    public SpawnValidator(BattlefieldController controller)
    {
        bfController = controller;
    }

    public bool CanDeployStackInTile(CreatureStack creatureToBeDeployed, TileController anchor)
    {
        CubeCoord AnchorCoord = anchor.Model.Coord;
        List<CubeCoord> shapeOffsets = shapeCatalog.GetShape(creatureToBeDeployed.Creature.Shape);
        foreach (CubeCoord offset in shapeOffsets)
        {
            CubeCoord coord = AnchorCoord + offset;
            if (!CheckTileExists(coord)) return false;
            TileController tile = bfController.BfGrid.TilesInTheBattlefield[coord];
            if (!CheckTileNotOccupied(tile)) return false;
            if (!CheckTileInDeployZone(tile)) return false;
        }
        return true;
    }

    private bool CheckTileExists(CubeCoord coord)
    => bfController.BfGrid.DoesTileExist(coord);

    private bool CheckTileNotOccupied(TileController tile)
        => tile.OccupantCreature == null;

    private bool CheckTileInDeployZone(TileController tile)
        => tile.Highlight.HasColorInLevel(2, deployColor);
}
