using System.Collections.Generic;
using UnityEngine;

public class HexHighlightController
{
    private readonly Dictionary<CubeCoord, TileModel> dicTileModels;

    // Constructor: recibe la referencia al mapa generado
    public HexHighlightController(Dictionary<CubeCoord, TileModel> tileModelsFromBattlefield)
    {
        this.dicTileModels = tileModelsFromBattlefield;
    }

    public void SetHighlight(CubeCoord coord, ETileHighlightType type)
    {
        if (dicTileModels.ContainsKey(coord))
            dicTileModels[coord].SetHighlight(type);
    }

    public void SetManyHighlights(IEnumerable<CubeCoord> coords, ETileHighlightType type)
    {
        foreach (var coord in coords)
            SetHighlight(coord, type);
    }

    public void ClearHighlightsByType(ETileHighlightType type)
    {
        foreach (var tileModel in dicTileModels)
        {
            if (tileModel.Value.Highlight == type)
                tileModel.Value.SetHighlight(ETileHighlightType.None);
        }
    }

    public void ClearAllHighlights()
    {
        foreach (var tileModel in dicTileModels)
            tileModel.Value.SetHighlight(ETileHighlightType.None);
    }
}
