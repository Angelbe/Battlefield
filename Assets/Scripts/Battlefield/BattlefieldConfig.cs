using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBattlefieldConfig
{
    int Rows { get; set; }
    int Cols { get; set; }
    float HexSize { get; set; }
    GameObject tilePrefab { get; set; }
    GameObject battlefieldPrefab { get; set; }
    TextAsset deploymentZonesJson { get; set; }
}


[CreateAssetMenu(fileName = "BattlefieldConfig", menuName = "Uroboros/BattlefieldConfig")]
public class BattlefieldConfig : ScriptableObject
{
    [Header("Battlefield Colors")]
    public Color noneColor = new(0f, 0f, 0f, 0.33f);
    public Color hoverColor = Color.cyan;
    public Color selectedColor = Color.yellow;
    public Color deployZoneColor = Color.green;
    [Header("Prefabs")]
    public GameObject tilePrefab;
    public GameObject battlefieldPrefab;
    [Header("Background")]
    public Sprite battlefieldBackgroundSprite;


    [Header("Battlefield Size")]
    public int Rows = 11;        // filas reales
    public int Cols = 19;        // columnas en filas pares
    public float HexSize = 0.55f;

    [Header("Deployment zones")]
    public TextAsset deploymentZonesJson;

    public Color GetColor(ETileHighlightType highlight)
    {
        return highlight switch
        {
            ETileHighlightType.Hover => hoverColor,
            ETileHighlightType.Selected => selectedColor,
            ETileHighlightType.DeployZone => deployZoneColor,
            ETileHighlightType.Transparent => noneColor,
            _ => noneColor,
        };
    }
}
