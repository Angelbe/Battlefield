using System.Collections.Generic;

public interface IBattlefieldMouseHandler
{
    public void HandleHoverTile(CubeCoord newTileCoordHovered);
    public void HandleUnhoverTile();
    public void HandleclickTile(CubeCoord TileClickedCoord);
}

public class BattlefieldMouseHandler : IBattlefieldMouseHandler
{
    private Dictionary<CubeCoord, TileController> tileControllers;
    private TileController currentTileHovered;
    private TileController currentTileClicked;

    public BattlefieldMouseHandler(Dictionary<CubeCoord, TileController> newTileControllers)
    {
        tileControllers = newTileControllers;
    }

    public void HandleHoverTile(CubeCoord newTileCoordHovered)
    {
        ETileHighlightType TileHoveredType = tileControllers[newTileCoordHovered].Highlight.currentHl;
        if (TileHoveredType == ETileHighlightType.Selected || TileHoveredType == ETileHighlightType.Hover)
        {
            return;
        }
        currentTileHovered = tileControllers[newTileCoordHovered];
        currentTileHovered.PaintTile(ETileHighlightType.Hover);
    }

    public void HandleUnhoverTile()
    {
        if (currentTileHovered == null || currentTileHovered.Highlight.currentHl == ETileHighlightType.Selected)
        {
            return;
        }
        currentTileHovered.ResetPaint();
        currentTileHovered = null;
    }
    public void HandleclickTile(CubeCoord TileClickedCoord)
    {
        if (currentTileClicked != null)
        {
            currentTileClicked.ResetPaint();
        }
        currentTileClicked = tileControllers[TileClickedCoord];
        currentTileClicked.PaintTile(ETileHighlightType.Selected);
    }
}