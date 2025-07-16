public class CombatPhase : IBattlePhase
{
    private BattlefieldController bfController;
    private UICombatController uICombatController;
    // public TurnHandler TurnHandler;

    public CombatPhase(BattlefieldController newBFController, UIController newUIController)
    {
        bfController = newBFController;
        uICombatController = newUIController.UICombatController;
    }
    public void StartPhase()
    {
        uICombatController.Init();
    }

    public void ExitPhase()
    {

    }
}