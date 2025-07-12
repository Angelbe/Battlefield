using System.Collections.Generic;
using UnityEngine;

public interface IBattlefieldController
{
    public void Init(BattlefieldConfig newBfConfig, BattlefieldModel newBfModel, SetupHelpers newsetupHelper);
    public void PaintManyTiles(IEnumerable<CubeCoord> coord, ETileHighlightType newHighlightType);
    public void GenerateGrid();
}

public class BattlefieldController : MonoBehaviour, IBattlefieldController
{
    private SetupHelpers setupHelpers;
    private BattlefieldModel bfModel;
    public BattlefieldConfig BfConfig { get; private set; }
    public BattlefieldHighlightHandler BfHighlight { get; private set; }
    public BattlefieldMouseHandler BfMouse { get; private set; }
    public BattlefieldGridHandler BfGrid { get; private set; }
    public Dictionary<CubeCoord, TileController> TileControllers = new();
    public Army ActiveArmy;

    public void GenerateGrid()
    {
        Transform gridContainer = transform.Find("GridContainer");
        Dictionary<CubeCoord, TileModel> tileModels = BfGrid.GenerateGridModel();
        foreach (TileModel tileModel in tileModels.Values)
        {
            var go = Instantiate(BfConfig.tilePrefab, tileModel.WorldPosition, Quaternion.identity, gridContainer);
            go.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
            var tileController = go.GetComponent<TileController>();
            tileController.Init(tileModel, this);
            TileControllers[tileModel.Coord] = tileController;
        }
    }

    public void GenerateCamera()
    {
        Vector2 center = GetGridWorldCenter();
        GameObject camGO = setupHelpers.CreateMainCamera(new Vector3(center.x, center.y, -10));
        camGO.transform.SetParent(transform);
    }

    public Vector2 GetGridWorldCenter()
    {
        Vector2 min = Vector2.positiveInfinity;
        Vector2 max = Vector2.negativeInfinity;

        foreach (var tileController in TileControllers.Values)
        {
            Vector2 pos = tileController.transform.position;
            min = Vector2.Min(min, pos);
            max = Vector2.Max(max, pos);
        }

        return (min + max) / 2f;
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

    public void Init(BattlefieldConfig newBfModel, BattlefieldModel newBfConfig, SetupHelpers newSetuphelpers)
    {
        setupHelpers = newSetuphelpers;
        bfModel = newBfConfig;
        BfConfig = newBfModel;
        SetActiveArmy(bfModel.Attacker);
        BfHighlight = new(TileControllers);
        BfMouse = new(TileControllers);
        BfGrid = new(BfConfig);
        GenerateGrid();
        GenerateCamera();
    }

}
