public interface IBattlePhase
{
    void StartPhase();                    // se llama al activar la fase
    void ExitPhase();                     // limpieza antes de cambiar de fase
}
