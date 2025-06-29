using System.Collections.Generic;
using UnityEngine;

public class BattlefieldController : MonoBehaviour
{
    public static BattlefieldController Instance { get; private set; }
    private BattlefieldModel bfModel;
    private BattlefieldConfig bfConfig;
    private PhaseManager phaseManager;
    public readonly Dictionary<CubeCoord, TileModel> TileModels = new();
    private readonly Dictionary<CubeCoord, TileView> tileViews = new();
    private HexHighlightController highlightController;
    private Dictionary<CubeCoord, CreatureModel> unitModels = new();
    public Dictionary<CreatureModel, CreatureView> UnitViews = new();
    private List<CubeCoord> currentDeployableTiles;
    private CubeCoord? selected;

    /* ---------- Generar grid ---------- */
    private void GenerateHexGrid()
    {
        for (int row = 0; row < bfConfig.GridHeight; row++)
        {
            int colsInRow = (row % 2 == 0) ? 19 : 18;

            for (int col = 0; col < colsInRow; col++)
            {
                CubeCoord cube = CubeCoord.FromOffset(col, row);
                var tileModel = new TileModel(cube);
                TileModels[cube] = tileModel;

                Vector2 pos = HexUtils.OffsetToWorld(col, row, bfConfig.HexSize);
                GameObject go = Instantiate(bfConfig.tilePrefab, pos, Quaternion.identity, transform);

                TileView tileView = go.GetComponent<TileView>();
                tileView.Init(tileModel);
                tileViews[cube] = tileView;
            }
        }
    }

    // ---------- ShowDeploymentZone ----------
    public void ShowDeploymentZone(bool isAttacker, EDeploymentLevel level)
    {
        currentDeployableTiles = DeploymentZone.GetZone(isAttacker, level);
        foreach (var tile in currentDeployableTiles)
        {
            if (TileModels.ContainsKey(tile) && !TileModels[tile].IsOccupied)
                TileModels[tile].SetHighlight(ETileHighlightType.DeployZone);  // color nuevo
        }
    }

    // ---------- Army interactions ----------
    public void ChangeActiveArmy(Army newArmy)
    {

    }

    // ---------- spawn ----------
    private void SpawnUnit(CubeCoord center, CubeCoord[] offsets)
    {
        // 1. Crear modelo temporal solo para calcular las coords
        var tempModel = new CreatureModel(center, offsets);
        var occupiedCoords = tempModel.OccupiedCoords;

        // 2. Verificar si TODAS las casillas están libres
        foreach (var coord in occupiedCoords)
        {
            if (!TileModels.ContainsKey(coord) || TileModels[coord].IsOccupied)
                return; // Alguna casilla no existe o está ocupada → no se puede spawnear
        }

        // 3. Crear modelo definitivo y registrar
        var unitModel = new CreatureModel(center, offsets);
        foreach (var coord in unitModel.OccupiedCoords)
        {
            TileModels[coord].SetOccupant(unitModel);
        }
        unitModels[center] = unitModel;

        // 4. Crear vista
        // Vector3 pos = WorldPosOf(center) + Vector3.up * 0.01f;
        // var go = Instantiate(unitPrefab, pos, Quaternion.identity, transform);
        // var unitView = go.GetComponent<CreatureView>();
        // unitView.Init(unitModel);
        // UnitViews[unitModel] = unitView;
    }


    /* ---------- Input público llamado desde TileView ---------- */
    public void OnTileClicked(CubeCoord cube)
    {
        if (selected.HasValue)
            highlightController.SetHighlight(selected.Value, ETileHighlightType.None);

        selected = cube;
        highlightController.SetHighlight(cube, ETileHighlightType.Selected);
    }

    public void OnTileHovered(CubeCoord cube) => TryHoverHighlight(cube, ETileHighlightType.Hover);
    public void OnTileUnhovered(CubeCoord cube) => TryHoverHighlight(cube, ETileHighlightType.None);

    public Vector3 WorldPosOf(CubeCoord cube)
    => HexUtils.CubeToWorld(cube, bfConfig.HexSize);

    private void TryHoverHighlight(CubeCoord cube, ETileHighlightType target)
    {
        if (selected.HasValue && selected.Value.Equals(cube)) return;
        highlightController.SetHighlight(cube, target);
    }


    public void Init(BattlefieldModel m, PhaseManager pm, BattlefieldConfig cfg)
    {
        bfModel = m;
        phaseManager = pm;
        bfConfig = cfg;
    }

    /* ---------- Ciclo de vida ---------- */
    private void Awake() => Instance = this;
    private void Start()
    {
        GenerateHexGrid();
        highlightController = new HexHighlightController(TileModels);
        ShowDeploymentZone(true, EDeploymentLevel.Basic);
        SpawnUnit(new CubeCoord(0, 0, 0), null);
    }

}
