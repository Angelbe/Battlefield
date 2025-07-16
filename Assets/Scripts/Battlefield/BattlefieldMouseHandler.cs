using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldMouseHandler : IBattlefieldMouseHandler
{
    private readonly Dictionary<CubeCoord, TileController> tileControllers;
    private readonly BattlefieldConfig config;

    private TileController currentTileHovered;
    private TileController currentTileClicked;

    public event Action<TileController> OnTileHovered;
    public event Action OnTileUnhovered;
    public event Action<TileController> OnTileClicked;

    private Color hoverColor => config.GetColor(ETileHighlightType.Hover);
    private Color selectedColor => config.GetColor(ETileHighlightType.Selected);

    public BattlefieldMouseHandler(Dictionary<CubeCoord, TileController> newTileControllers, BattlefieldConfig battlefieldConfig)
    {
        tileControllers = newTileControllers;
        config = battlefieldConfig;
    }

    public void HandleHoverTile(CubeCoord newTileCoordHovered)
    {
        var tile = tileControllers[newTileCoordHovered];
        if (tile == currentTileClicked)
            return; // ya está seleccionada, no aplicar hover

        currentTileHovered = tile;
        currentTileHovered.Highlight.AddColor(4, hoverColor);
        OnTileHovered?.Invoke(currentTileHovered);
    }

    public void HandleUnhoverTile()
    {
        if (currentTileHovered == null || currentTileHovered == currentTileClicked)
            return;

        currentTileHovered.Highlight.RemoveColor(4, hoverColor);
        currentTileHovered = null;
        OnTileUnhovered?.Invoke();
    }

    public void HandleClickTile(CubeCoord tileClickedCoord)
    {
        if (currentTileClicked != null)
        {
            currentTileClicked.Highlight.RemoveColor(5, selectedColor);
        }

        currentTileClicked = tileControllers[tileClickedCoord];
        currentTileClicked.Highlight.AddColor(5, selectedColor);
        OnTileClicked?.Invoke(currentTileClicked);
    }
}
