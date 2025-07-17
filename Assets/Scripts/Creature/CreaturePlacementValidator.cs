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
        var shapeOffsets = shapeCatalog.GetShape(creature.Shape);
        foreach (var offset in shapeOffsets)
        {
            var coord = anchor + offset;
            if (!CheckTileExists(coord)) return false;

            var tile = bfController.TileControllers[coord];
            if (!CheckTileNotOccupied(tile)) return false;
            if (!CheckTileInDeployZone(tile)) return false;
        }
        return true;
    }

    private bool CheckTileExists(CubeCoord coord)
        => bfController.TileControllers.ContainsKey(coord);

    private bool CheckTileNotOccupied(TileController tile)
        => tile.OccupantCreature == null;

    private bool CheckTileInDeployZone(TileController tile)
        => tile.Highlight.HasColorInLevel(2, deployColor);
}
