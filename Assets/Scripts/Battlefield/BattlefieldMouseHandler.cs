using System;
using System.Collections.Generic;

public interface IBattlefieldMouseHandler
{
    public void HandleHoverTile(CubeCoord newTileCoordHovered);
    public void HandleUnhoverTile();
    public void HandleClickTile(CubeCoord TileClickedCoord);
}

public class BattlefieldMouseHandler : IBattlefieldMouseHandler
{
    public Dictionary<CubeCoord, TileController> TileControllers;
    private TileController currentTileHovered;
    private TileController currentTileClicked;
    public event Action<TileController> OnTileHovered;
    public event Action OnTileUnhovered;
    public event Action<TileController> OnTileClicked;


    public BattlefieldMouseHandler(Dictionary<CubeCoord, TileController> newTileControllers)
    {
        TileControllers = newTileControllers;
    }

    public void HandleHoverTile(CubeCoord newTileCoordHovered)
    {
        ETileHighlightType TileHoveredType = TileControllers[newTileCoordHovered].Highlight.currentHl;
        if (TileHoveredType == ETileHighlightType.Selected || TileHoveredType == ETileHighlightType.Hover)
        {
            return;
        }
        currentTileHovered = TileControllers[newTileCoordHovered];
        currentTileHovered.PaintTile(ETileHighlightType.Hover);
        OnTileHovered?.Invoke(currentTileHovered);
    }

    public void HandleUnhoverTile()
    {
        if (currentTileHovered == null || currentTileHovered.Highlight.currentHl == ETileHighlightType.Selected)
        {
            return;
        }
        currentTileHovered.ResetPaint();
        currentTileHovered = null;
        OnTileUnhovered?.Invoke();
    }
    public void HandleClickTile(CubeCoord TileClickedCoord)
    {
        if (currentTileClicked != null)
        {
            currentTileClicked.ResetPaint();
        }
        currentTileClicked = TileControllers[TileClickedCoord];
        currentTileClicked.PaintTile(ETileHighlightType.Selected);
        OnTileClicked?.Invoke(currentTileClicked);
    }
}