using System.Collections.Generic;
using UnityEngine;

public class BattlefieldController : MonoBehaviour
{
    public static BattlefieldController Instance { get; private set; }

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int gridHeight = 11;
    [SerializeField] private float hexSize = 1f;
    [SerializeField] private GameObject unitPrefab;
    public readonly Dictionary<CubeCoord, TileModel> TileModels = new();
    private readonly Dictionary<CubeCoord, TileView> tileViews = new();
    private Dictionary<CubeCoord, UnitModel> unitModels = new();
    public Dictionary<UnitModel, UnitView> UnitViews = new();
    private CubeCoord? selected;    // casilla actualmente seleccionada
    private UnitModel selectedUnit;

    /* ---------- Ciclo de vida ---------- */
    private void Awake() => Instance = this;
    private void Start()
    {
        GenerateHexGrid();
        SpawnUnit(new CubeCoord(0, 0, 0), null);

    }

    /* ---------- Generar grid ---------- */
    private void GenerateHexGrid()
    {
        for (int row = 0; row < gridHeight; row++)
        {
            int colsInRow = (row % 2 == 0) ? 19 : 18;

            for (int col = 0; col < colsInRow; col++)
            {
                CubeCoord cube = CubeCoord.FromOffset(col, row);
                var model = new TileModel(cube);
                TileModels[cube] = model;

                Vector2 pos = HexUtils.OffsetToWorld(col, row, hexSize);
                GameObject go = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                TileView view = go.GetComponent<TileView>();
                view.Init(model);
                tileViews[cube] = view;
            }
        }
    }

    // ---------- spawn ----------
    private void SpawnUnit(CubeCoord center, CubeCoord[] offsets)
    {
        // 1. Crear modelo temporal solo para calcular las coords
        var tempModel = new UnitModel(center, offsets);
        var occupiedCoords = tempModel.OccupiedCoords;

        // 2. Verificar si TODAS las casillas están libres
        foreach (var coord in occupiedCoords)
        {
            if (!TileModels.ContainsKey(coord) || TileModels[coord].IsOccupied)
                return; // Alguna casilla no existe o está ocupada → no se puede spawnear
        }

        // 3. Crear modelo definitivo y registrar
        var unitModel = new UnitModel(center, offsets);
        foreach (var coord in unitModel.OccupiedCoords)
        {
            TileModels[coord].SetOccupant(unitModel);
        }
        unitModels[center] = unitModel;

        // 4. Crear vista
        Vector3 pos = WorldPosOf(center) + Vector3.up * 0.01f;
        var go = Instantiate(unitPrefab, pos, Quaternion.identity, transform);
        var unitView = go.GetComponent<UnitView>();
        unitView.Init(unitModel);
        UnitViews[unitModel] = unitView;
    }


    /* ---------- Input público llamado desde TileView ---------- */
    public void OnTileClicked(CubeCoord cube)
    {
        if (selected.HasValue)
            SetHighlightKey(selected.Value, TileHighlightType.None);

        selected = cube;
        SetHighlightKey(cube, TileHighlightType.Selected);
    }

    public void OnTileHovered(CubeCoord cube) => TryTempHighlight(cube, TileHighlightType.Hover);
    public void OnTileUnhovered(CubeCoord cube) => TryTempHighlight(cube, TileHighlightType.None);

    /* ---------- Helpers ---------- */
    private void SetHighlightKey(CubeCoord cube, TileHighlightType colorKey)
    {
        var tileModel = TileModels[cube];
        tileModel.SetHighlight(colorKey);
    }

    public Vector3 WorldPosOf(CubeCoord cube)
    => HexUtils.CubeToWorld(cube, hexSize);

    private void TryTempHighlight(CubeCoord cube, TileHighlightType target)
    {
        if (selected.HasValue && selected.Value.Equals(cube)) return;
        SetHighlightKey(cube, target);
    }

}
