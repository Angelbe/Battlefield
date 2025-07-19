using System.Collections.Generic;
using UnityEngine;

public class CreatureValidatorHandler
{
    private readonly BattlefieldController bfController;
    private readonly CreatureShapeCatalog shapeCatalog;
    private readonly CreatureController creatureController;

    public CreatureValidatorHandler(CreatureController newCreatureController, BattlefieldController newBfController, CreatureShapeCatalog newShapeCatalog)
    {
        creatureController = newCreatureController;
        bfController = newBfController;
        shapeCatalog = newShapeCatalog;
    }

    // Validación puramente de forma y espacio
    public bool DoesTheCreatureFitInTile(TileController anchor)
    {
        CubeCoord AnchorCoord = anchor.Model.Coord;
        List<CubeCoord> shapeOffsets = shapeCatalog.GetShape(creatureController.Model.Shape);
        foreach (CubeCoord offset in shapeOffsets)
        {
            CubeCoord coord = AnchorCoord + offset;
            if (!CheckTileExists(coord)) return false;

            TileController tile = bfController.BfGrid.TilesInTheBattlefield[coord];
            if (!CheckTileNotOccupied(tile)) return false;
        }
        return true;
    }

    private bool CheckTileExists(CubeCoord coord)
        => bfController.BfGrid.DoesTileExist(coord);

    private bool CheckTileNotOccupied(TileController tile)
        => tile.OccupantCreature == null;
}
