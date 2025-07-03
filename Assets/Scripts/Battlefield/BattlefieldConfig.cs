using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattlefieldConfig", menuName = "Uroboros/BattlefieldConfig")]
public class BattlefieldConfig : ScriptableObject
{
    public Color noneColor = new (0f, 0f, 0f, 0.5f);
    public Color hoverColor = Color.cyan;
    public Color selectedColor = Color.yellow;
    public Color deployZoneColor = Color.green;
    public GameObject tilePrefab;
    public GameObject battlefieldPrefab;
    public float HexSize = 0.55f;

    /* -- Datos de tablero -- */
    public int Rows = 11;        // filas reales
    public int Cols = 19;        // columnas en filas pares

    /* -- RANGOS de despliegue -- */
    [Header("Deployment zones (cube coords)")]
    public List<CubeCoord> attackerBasic = new();
    public List<CubeCoord> defenderBasic = new();

    // Opcional: advanced/expert/master
    // public List<CubeCoord> attackerAdvanced = new();
    public Color GetColor(ETileHighlightType highlight)
    {
        return highlight switch
        {
            ETileHighlightType.Hover => hoverColor,
            ETileHighlightType.Selected => selectedColor,
            ETileHighlightType.DeployZone => deployZoneColor,
            _ => noneColor,
        };
    }
}
