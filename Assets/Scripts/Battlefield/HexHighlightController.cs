using System.Collections.Generic;
using UnityEngine;

public class HexHighlightController
{
    private readonly Dictionary<CubeCoord, TileController> tileControllers;

    public HexHighlightController(Dictionary<CubeCoord, TileController> tileControllersFromBattlefield)
    {
        this.tileControllers = tileControllersFromBattlefield;
    }

    public void SetManyHighlights(IEnumerable<CubeCoord> coords, ETileHighlightType type)
    {
        foreach (var coord in coords)
            tileControllers[coord].SetHighlight(type);
    }

    public void ClearHighlightsByType(ETileHighlightType type)
    {
        foreach (var tileModel in tileControllers)
        {
            if (tileModel.Value.BaseKey == type)
                tileModel.Value.SetHighlight(ETileHighlightType.None, true);
        }
    }

    public void ClearAllHighlights()
    {
        foreach (var tileModel in tileControllers)
            tileModel.Value.SetHighlight(ETileHighlightType.None);
    }
}
