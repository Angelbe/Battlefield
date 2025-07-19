using System.Collections.Generic;
using UnityEngine;

public class CreaturePlacementValidator
{
    private readonly BattlefieldController bfController;
    private readonly CreatureShapeCatalog shapeCatalog;
    private readonly Color deployColor;

    public CreaturePlacementValidator(BattlefieldController bf, CreatureShapeCatalog shapeCatalog, Color deployColor)
    {
        this.bfController = bf;
        this.shapeCatalog = shapeCatalog;
        this.deployColor = deployColor;
    }

    public bool DoesTheCreatureFitInTile(CreatureModel creature, CubeCoord anchor)
    {
        List<CubeCoord> shapeOffsets = shapeCatalog.GetShape(creature.Shape);
        foreach (var offset in shapeOffsets)
        {
            CubeCoord coord = anchor + offset;
            if (!CheckTileExists(coord)) return false;

            TileController tile = bfController.BfGrid.TilesInTheBattlefield[coord];
            if (!CheckTileNotOccupied(tile)) return false;
            if (!CheckTileInDeployZone(tile)) return false;
        }
        return true;
    }

    private bool CheckTileExists(CubeCoord coord)
        => bfController.BfGrid.TilesInTheBattlefield.ContainsKey(coord);

    private bool CheckTileNotOccupied(TileController tile)
        => tile.OccupantCreature == null;

    private bool CheckTileInDeployZone(TileController tile)
        => tile.Highlight.HasColorInLevel(2, deployColor);
}
