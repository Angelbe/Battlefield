using System.Collections.Generic;
using UnityEngine;

public class BattlefieldController : MonoBehaviour, IBattlefieldController
{
    [SerializeField]
    private GameObject BattlefieldSpawnPrefab;
    public PhaseManager PhaseManager { get; private set; }
    public BattlefieldModel bfModel { get; private set; }
    public Vector2 Center { get; private set; }
    public BattlefieldConfig BfConfig { get; private set; }
    public BattlefieldSpawnController BfSpawn { get; private set; }
    public BattlefieldHighlightHandler BfHighlight { get; private set; }
    public BattlefieldMouseHandler BfMouse { get; private set; }
    public BattlefieldGridHandler BfGrid { get; private set; }
    public BattlefieldDeploymentZones BfDeploymentZones { get; private set; }
    public Dictionary<CubeCoord, TileController> TileControllers { get; private set; } = new();
    public Transform GridContainer;

    public void GenerateGrid()
    {
        Dictionary<CubeCoord, TileModel> tileModels = BfGrid.GenerateGridModel();
        foreach (TileModel tileModel in tileModels.Values)
        {
            var go = Instantiate(BfConfig.tilePrefab, tileModel.WorldPosition, Quaternion.identity, GridContainer);
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
        bg.transform.SetParent(transform);
        bg.transform.position = Center;
        float scaleX = 1.04f;
        float scaleY = 1.04f;

        bg.transform.localScale = new Vector3(scaleX, scaleY, 1f);

        SpriteRenderer spriteRenderer = bg.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = BfConfig.battlefieldBackgroundSprite;
        spriteRenderer.sortingOrder = -10;
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


    public void HandleHoverTile(TileController tileHovered)
    {
        BfMouse.HandleHoverTile(tileHovered);
    }

    public void HandleUnhoverTile()
    {
        BfMouse.HandleUnhoverTile();
    }
    public void HandleClickTile(TileController tileClicked)
    {
        BfMouse.HandleClickTile(tileClicked);
    }

    public void Init(
     BattlefieldModel newBfModel,
     BattlefieldConfig newBfConfig,
     CursorBattlefieldController newCursor,
     PhaseManager newPhaseManager,
     CreatureCatalog creatureCatalog,
     UIController newUiDeployController)
    {
        BfConfig = newBfConfig;
        bfModel = newBfModel;
        PhaseManager = newPhaseManager;
        BfMouse = new(TileControllers, BfConfig, PhaseManager, newCursor);
        BfGrid = new(BfConfig);
        BfDeploymentZones = new(this, BfConfig);
        BfHighlight = new(TileControllers, BfDeploymentZones, BfConfig, bfModel);
        GameObject BfSpawnGO = Instantiate(BattlefieldSpawnPrefab, transform);
        BfSpawnGO.name = "Units";
        BfSpawn = BfSpawnGO.GetComponent<BattlefieldSpawnController>();
        BfSpawn.Init(this, creatureCatalog, newUiDeployController.UIDeployController, BfMouse);
        GenerateGrid();
        GenerateBattlefieldBackground();
    }

}
