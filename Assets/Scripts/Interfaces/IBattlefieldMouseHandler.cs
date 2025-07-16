public interface IBattlefieldMouseHandler
{
    public void HandleHoverTile(CubeCoord newTileCoordHovered);
    public void HandleUnhoverTile();
    public void HandleClickTile(CubeCoord TileClickedCoord);
}