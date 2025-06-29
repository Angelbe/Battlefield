public class PhaseManager
{
    private IBattlePhase current;
    private BattlefieldModel bfModel;

    public PhaseManager(BattlefieldModel newBfModel)
    {
        bfModel = newBfModel;
    }

    public void StartBattle(BattlefieldController newBfController, Army attacker, Army defender)
    {
        // current = new DeployPhaseController();
        // current.EnterPhase();
    }

    public void ChangePhase(EBattlePhase nextPhase)
    {
        current.ExitPhase();

        current = nextPhase switch
        {
            // EBattlePhase.Combat => new CombatPhaseController(bf, hh, attacker, defender),
            // EBattlePhase.Results => new ResultsPhaseController(bf, hh, attacker, defender),
            _ => current // no debería ocurrir
        };
        current.EnterPhase();
    }
}
