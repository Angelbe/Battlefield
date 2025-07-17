public interface IBattlefieldMouseHandler
{
    public void HandleHoverTile(TileController tileHovered);
    public void HandleUnhoverTile();
    public void HandleClickTile(TileController tileClicked);
}