public class CombatPhase : IBattlePhase
{
    private BattlefieldController bfController;
    private UICombatController uICombatController;
    public TurnHandler TurnHandler;
    public DeployHandler AttackerCreatures;
    public DeployHandler DefenderCreatures;
    public CreatureController ActiveCreature;

    public CombatPhase(BattlefieldController newBFController, UIController newUIController)
    {
        bfController = newBFController;
        uICombatController = newUIController.UICombatController;
        AttackerCreatures = bfController.bfModel.Attacker.Deployed;
        DefenderCreatures = bfController.bfModel.Defender.Deployed;
        TurnHandler = new(AttackerCreatures, DefenderCreatures);
    }
    public void StartPhase()
    {
        uICombatController.Init();
    }

    public void ExitPhase()
    {
        uICombatController.Shutdown();
    }
}