using System.Collections.Generic;
using UnityEngine;

public class BattlefieldController : MonoBehaviour, IBattlefieldController
{
    [SerializeField]
    private GameObject BattlefieldSpawnPrefab;
    public PhaseManager PhaseManager { get; private set; }
    public BattlefieldModel bfModel { get; private set; }
    public BattlefieldConfig BfConfig { get; private set; }
    public BattlefieldSpawnController BfSpawn { get; private set; }
    public BattlefieldHighlightHandler BfHighlight { get; private set; }
    public BattlefieldMouseHandler BfMouse { get; private set; }
    public BattlefieldGridHandler BfGrid { get; private set; }
    public BattlefieldDeploymentZones BfDeploymentZones { get; private set; }
    public Transform GridContainer;

    public void GenerateBattlefieldBackground()
    {
        if (BfConfig.battlefieldBackgroundSprite == null)
        {
            Debug.LogWarning("No se ha asignado un sprite de fondo en BattlefieldConfig.");
            return;
        }

        GameObject bg = new GameObject("BattlefieldBackground");
        bg.transform.SetParent(transform);
        bg.transform.position = BfGrid.Center;
        float scaleX = 1.04f;
        float scaleY = 1.04f;

        bg.transform.localScale = new Vector3(scaleX, scaleY, 1f);

        SpriteRenderer spriteRenderer = bg.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = BfConfig.battlefieldBackgroundSprite;
        spriteRenderer.sortingOrder = -10;
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
        BfGrid = new(BfConfig, this);
        BfMouse = new(BfGrid.TilesInTheBattlefield, BfConfig, PhaseManager, newCursor);
        BfDeploymentZones = new(this, BfConfig);
        BfHighlight = new(BfGrid.TilesInTheBattlefield, BfDeploymentZones, BfConfig, bfModel);
        GameObject BfSpawnGO = Instantiate(BattlefieldSpawnPrefab, transform);
        BfSpawnGO.name = "Units";
        BfSpawn = BfSpawnGO.GetComponent<BattlefieldSpawnController>();
        BfSpawn.Init(this, creatureCatalog, newUiDeployController.UIDeployController, BfMouse);
        BfGrid.GenerateGrid();
        GenerateBattlefieldBackground();
    }

}
