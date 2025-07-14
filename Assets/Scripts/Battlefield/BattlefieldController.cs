using System.Collections.Generic;
using UnityEngine;

public interface IBattlefieldController
{
    public Vector2 Center { get; }
    public BattlefieldConfig BfConfig { get; }
    public BattlefieldHighlightHandler BfHighlight { get; }
    public BattlefieldMouseHandler BfMouse { get; }
    public BattlefieldGridHandler BfGrid { get; }
    public BattlefieldDeploymentHandler BfDeploymentZones { get; }
    public Dictionary<CubeCoord, TileController> TileControllers { get; }
    public Army ActiveArmy { get; }
    public void Init(BattlefieldConfig newBfConfig, BattlefieldModel newBfModel, SetupHelpers newsetupHelper);
    public void PaintManyTiles(IEnumerable<CubeCoord> coord, ETileHighlightType newHighlightType);
    public void GenerateGrid();
}

public class BattlefieldController : MonoBehaviour, IBattlefieldController
{
    private SetupHelpers setupHelpers;
    public BattlefieldModel bfModel { get; private set; }
    public Vector2 Center { get; private set; }
    public BattlefieldConfig BfConfig { get; private set; }
    public BattlefieldHighlightHandler BfHighlight { get; private set; }
    public BattlefieldMouseHandler BfMouse { get; private set; }
    public BattlefieldGridHandler BfGrid { get; private set; }
    public BattlefieldDeploymentHandler BfDeploymentZones { get; private set; }
    public Dictionary<CubeCoord, TileController> TileControllers { get; private set; } = new();
    public Army ActiveArmy { get; private set; }

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
        Center = GetGridWorldCenter();
    }

    public void GenerateBattlefieldBackground()
    {
        if (BfConfig.battlefieldBackgroundSprite == null)
        {
            Debug.LogWarning("No se ha asignado un sprite de fondo en BattlefieldConfig.");
            return;
        }

        GameObject bg = new GameObject("BattlefieldBackground");
        bg.transform.SetParent(transform); // lo agrupa en la jerarquía del battlefield
        bg.transform.position = Center; // o ajusta según la escala del mapa
        bg.transform.position = Center; // o ajusta según la escala del mapa
        float scaleX = 1.04f;
        float scaleY = 1.04f;

        bg.transform.localScale = new Vector3(scaleX, scaleY, 1f);

        var sr = bg.AddComponent<SpriteRenderer>();
        sr.sprite = BfConfig.battlefieldBackgroundSprite;
        sr.sortingOrder = -10; // para que quede por debajo de las tiles
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
        BfHighlight.SetManyHl(coord, newHighlightType);
    }

    public void PaintManyOriginalTiles(IEnumerable<CubeCoord> coord, ETileHighlightType newHighlightType)
    {
        BfHighlight.SetManyOriginalHl(coord, newHighlightType);
    }
    public void ResetManyTilesWithType(ETileHighlightType newHighlightType)
    {
        BfHighlight.SetManyToBase(newHighlightType);
    }

    public void PaintAttackerDeploymentZone()
    {
        PaintManyOriginalTiles(BfDeploymentZones.AttackerZones[bfModel.Attacker.Champion.DeploymentLevel], ETileHighlightType.DeployZone);
    }

    public void PaintDefenderDeploymentZone()
    {
        PaintManyOriginalTiles(BfDeploymentZones.DefenderZones[bfModel.Attacker.Champion.DeploymentLevel], ETileHighlightType.DeployZone);
    }

    public void ClearDeploymentZones()
    {
        ResetManyTilesWithType(ETileHighlightType.DeployZone);
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
        GenerateBattlefieldBackground();
        BfDeploymentZones = new(this, BfConfig);
    }

}
