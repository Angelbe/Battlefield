public interface IBattlePhase
{
    void EnterPhase();                    // se llama al activar la fase
    void ExitPhase();                     // limpieza antes de cambiar de fase
}
