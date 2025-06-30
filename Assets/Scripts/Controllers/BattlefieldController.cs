using System.Collections.Generic;
using UnityEngine;

public class BattlefieldController : MonoBehaviour
{
    public static BattlefieldController Instance { get; private set; }
    private BattlefieldModel bfModel;
    public BattlefieldConfig bfConfig;
    private PhaseManager phaseManager;
    public readonly Dictionary<CubeCoord, TileController> TileCtrls = new();
    private Dictionary<CubeCoord, CreatureModel> unitModels = new();
    public Dictionary<CreatureModel, CreatureView> UnitViews = new();
    private CubeCoord? selected;

    /* ---------- Generar grid ---------- */
    public void GenerateHexGrid()
    {
        for (int row = 0; row < bfConfig.Rows; row++)
        {
            int colsInRow = (row & 1) == 0 ? 19 : 18;

            for (int col = 0; col < colsInRow; col++)
            {
                CubeCoord cube = CubeCoord.FromOffset(col, row);
                var TileModel = new TileModel(cube);

                Vector2 pos = HexUtils.OffsetToWorld(col, row, bfConfig.HexSize);
                var go = Instantiate(bfConfig.tilePrefab, pos, Quaternion.identity, transform);

                var ctrl = go.GetComponent<TileController>();
                ctrl.Init(TileModel);
                TileCtrls[cube] = ctrl;
            }
        }
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
            if (!TileCtrls.ContainsKey(coord) || TileCtrls[coord].Model.IsOccupied)
                return; // Alguna casilla no existe o está ocupada → no se puede spawnear
        }

        // 3. Crear modelo definitivo y registrar
        var unitModel = new CreatureModel(center, offsets);
        foreach (var coord in unitModel.OccupiedCoords)
        {
            TileCtrls[coord].Model.SetOccupant(unitModel);
        }
        unitModels[center] = unitModel;

        // 4. Crear vista
        // Vector3 pos = WorldPosOf(center) + Vector3.up * 0.01f;
        // var go = Instantiate(unitPrefab, pos, Quaternion.identity, transform);
        // var unitView = go.GetComponent<CreatureView>();
        // unitView.Init(unitModel);
        // UnitViews[unitModel] = unitView;
    }


    /* ---------- Click y Hover ---------- */
    public void OnTileClicked(CubeCoord cube)
    {
        if (selected.HasValue)
            TileCtrls[selected.Value].ResetHighlight();

        selected = cube;
        TileCtrls[cube].SetHighlight(ETileHighlightType.Selected);
    }
    public void OnTileHovered(CubeCoord c) => TileCtrls[c].SetHighlight(ETileHighlightType.Hover);
    public void OnTileUnhovered(CubeCoord c) => TileCtrls[c].ResetHighlight();
    public Vector3 WorldPosOf(CubeCoord cube)
    {
        return HexUtils.CubeToWorld(cube, bfConfig.HexSize);
    }

    public void Init(BattlefieldModel m, BattlefieldConfig cfg)
    {
        bfModel = m;
        bfConfig = cfg;
    }

    public void setPhaseManager(PhaseManager pm)
    {
        phaseManager = pm;
    }

    /* ---------- Ciclo de vida ---------- */
    private void Awake() => Instance = this;
    private void Start()
    {

    }


}
