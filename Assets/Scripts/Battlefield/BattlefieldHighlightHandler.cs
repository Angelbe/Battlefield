using System.Collections.Generic;
using UnityEngine;

public class BattlefieldHighlightHandler
{
    private Dictionary<CubeCoord, TileController> tileControllers;

    public BattlefieldHighlightHandler(Dictionary<CubeCoord, TileController> tileControllersFromBattlefield)
    {
        tileControllers = tileControllersFromBattlefield;
    }

    public void SetManyOriginalHl(IEnumerable<CubeCoord> coords, ETileHighlightType type)
    {
        foreach (var coord in coords)
            tileControllers[coord].PaintOriginalHlTile(type);
    }

    public void SetManyHl(IEnumerable<CubeCoord> coords, ETileHighlightType type)
    {
        foreach (var coord in coords)
            tileControllers[coord].PaintTile(type);
    }

    public void SetManyToBase(ETileHighlightType typeToFilter)
    {
        foreach (TileController tileController in tileControllers.Values)
        {
            if (tileController.Highlight.currentHl == typeToFilter)
            {
                tileController.ResetPaint();
            }
        }
    }

    public void ClearAllHighlights()
    {
        foreach (TileController tileController in tileControllers.Values)
            tileController.ResetPaint();
    }
}
