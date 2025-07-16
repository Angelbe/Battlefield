public interface ITileController
{
    BattlefieldController BfController { get; }
    TileView View { get; }
    TileModel Model { get; }
    TileHighlightController Highlight { get; }
}