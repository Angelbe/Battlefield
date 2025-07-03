using System.Collections.Generic;
using UnityEngine;

public interface IBattlefieldController
{
    public void Init(BattlefieldConfig newBfConfig, BattlefieldModel newBfModel);
    public void PaintManyTiles(IEnumerable<CubeCoord> coord, ETileHighlightType newHighlightType);
    public void GenerateGrid();
}

public class BattlefieldController : MonoBehaviour, IBattlefieldController
{
    private BattlefieldModel bfModel;
    public BattlefieldConfig BfConfig { get; private set; }
    public BattlefieldHighlightHandler BfHighlight { get; private set; }
    public BattlefieldMouseHandler BfMouse { get; private set; }
    public BattlefieldGridHandler BfGrid { get; private set; }
    public Dictionary<CubeCoord, TileController> TileControllers = new();
    public Army ActiveArmy;

    public void GenerateGrid()
    {
        Dictionary<CubeCoord, TileModel> tileModels = BfGrid.GenerateGridModel();
        foreach (TileModel tileModel in tileModels.Values)
        {
            var go = Instantiate(BfConfig.tilePrefab, tileModel.WorldPosition, Quaternion.identity, transform);
            var TileController = go.GetComponent<TileController>();
            TileController.Init(tileModel, this);
            TileControllers[tileModel.Coord] = TileController;
        }

        // for (int row = 0; row < BfConfig.Rows; row++)
        // {
        //     int colsInRow = (row & 1) == 0 ? 19 : 18;

        //     for (int col = 0; col < colsInRow; col++)
        //     {
        //         CubeCoord cube = CubeCoord.FromColRow(col, row);

        //         Vector2 pos = TileUtils.OffsetToWorld(col, row, BfConfig.HexSize);
        //         var go = Instantiate(BfConfig.tilePrefab, pos, Quaternion.identity, transform);
        //         var TileModel = new TileModel(cube, pos, new ColRow (row, col), BfConfig.HexSize);

        //         var TileController = go.GetComponent<TileController>();
        //         TileController.Init(TileModel, this);
        //         TileControllers[cube] = TileController;
        //     }
        // }
    }

    public Vector3 WorldPosOf(CubeCoord cube)
    {
        return TileUtils.CubeToWorld(cube, BfConfig.HexSize);
    }

    public void SetActiveArmy(Army newActiveArmy)
    {
        ActiveArmy = newActiveArmy;
    }

    public void HandleHoverTile(CubeCoord newTileCoordHovered)
    {
        BfMouse.HandleHoverTile(newTileCoordHovered);
    }

    public void HandleUnhoverTile()
    {
        BfMouse.HandleUnhoverTile();
    }
    public void HandleclickTile(CubeCoord TileClickedCoord)
    {
        BfMouse.HandleclickTile(TileClickedCoord);
    }
    public void PaintManyTiles(IEnumerable<CubeCoord> coord, ETileHighlightType newHighlightType)
    {
        BfHighlight.SetManyHighlights(coord, newHighlightType);
    }
    public void ResetManyTilesWithType(ETileHighlightType newHighlightType)
    {
        BfHighlight.SetManyToBase(newHighlightType);
    }

    public void Init(BattlefieldConfig newBfModel, BattlefieldModel newBfConfig)
    {
        bfModel = newBfConfig;
        BfConfig = newBfModel;
        SetActiveArmy(bfModel.Attacker);
        BfHighlight = new(TileControllers);
        BfMouse = new(TileControllers);
        BfGrid = new(BfConfig);
        GenerateGrid();
    }

}
