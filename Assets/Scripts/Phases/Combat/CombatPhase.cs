public class CombatPhase : IBattlePhase
{
    private BattlefieldController bfController;
    private UICombatController uICombatController;
    public TurnHandler TurnHandler;
    public DeployHandler AttackerCreatures;
    public DeployHandler DefenderCreatures;
    public CreatureController ActiveCreature;
    public Army ActiveArmy;

    public CombatPhase(BattlefieldController newBFController, UIController newUIController)
    {
        bfController = newBFController;
        uICombatController = newUIController.UICombatController;
    }
    public void StartPhase()
    {
        AttackerCreatures = bfController.bfModel.Attacker.Deployed;
        DefenderCreatures = bfController.bfModel.Defender.Deployed;
        TurnHandler = new(AttackerCreatures, DefenderCreatures);
        uICombatController.Init();
        ActiveCreature = TurnHandler.PeekCurrentCreature();
    }

    public void HandleCreatureFinishedTurn()
    {
        ActiveCreature = TurnHandler.GetNextCreature();
    }

    public void ExitPhase()
    {
        uICombatController.Shutdown();
    }
}