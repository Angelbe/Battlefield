using System.Collections.Generic;
using UnityEngine;

public class BattlefieldController : MonoBehaviour
{
    public static BattlefieldController Instance { get; private set; }
    private BattlefieldModel bfModel;
    public BattlefieldConfig bfConfig { get; private set; }
    public readonly Dictionary<CubeCoord, TileController> TileCtrls = new();
    public Army ActiveArmy;
    private readonly Dictionary<CubeCoord, CreatureModel> deployed = new();
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

    public Vector3 WorldPosOf(CubeCoord cube)
    {
        return HexUtils.CubeToWorld(cube, bfConfig.HexSize);
    }

    public void SetActiveArmy(Army newActiveArmy)
    {
        ActiveArmy = newActiveArmy;
    }

    public CreatureModel GetCreatureAt(CubeCoord coord)
        => deployed.TryGetValue(coord, out var c) ? c : null;

    public void Init(BattlefieldModel m, BattlefieldConfig cfg)
    {
        bfModel = m;
        bfConfig = cfg;
        SetActiveArmy(bfModel.Attacker);
    }

}
