public interface IBattlePhase
{
    void EnterPhase();                    // se llama al activar la fase
    void ExitPhase();                     // limpieza antes de cambiar de fase
    // void OnTileClicked(CubeCoord coord);  // input principal
    void OnTileHovered(CubeCoord coord);
    void OnTileUnhovered(CubeCoord coord);
}
