using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattlefieldConfig", menuName = "Uroboros/BattlefieldConfig")]
public class BattlefieldConfig : ScriptableObject
{
    [Header("Battlefield Colors")]
    [SerializeField] private List<HighlightColorEntry> highlightColors;

    private Dictionary<ETileHighlightType, Color> colorLookup;

    [Serializable]
    private struct HighlightColorEntry
    {
        public ETileHighlightType type;
        public Color color;
    }
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

    [Header("Cursor attack")]
    public GameObject CursorSword;
    public Texture2D CursorArrow;

    public Color GetColor(ETileHighlightType type)
    {
        return colorLookup.TryGetValue(type, out var color) ? color : Color.clear;
    }

    private void OnEnable()
    {
        colorLookup = new();
        foreach (var entry in highlightColors)
            colorLookup[entry.type] = entry.color;
    }
}
