using System.Diagnostics;

public class PhaseManager
{
    private IBattlePhase currentPhase;
    public readonly DeploymentPhase DeploymentPhase;
    public readonly CombatPhase CombatPhase;
    public UIController UIController { get; private set; }
    private readonly BattlefieldController bfController;

    public PhaseManager(BattlefieldController controller, UIController newUIController)
    {
        UIController = newUIController;
        bfController = controller;
        DeploymentPhase = new DeploymentPhase(bfController, UIController, this);
        CombatPhase = new CombatPhase(bfController, UIController);
    }
    public void StartBattle()
    {
        CombatPhase.ExitPhase();
        currentPhase = DeploymentPhase;
        DeploymentPhase.StartPhase();
    }

    public void FinishDeploymentPhase()
    {
        currentPhase.ExitPhase();
        StartCombatPhase();
    }

    public void StartCombatPhase()
    {
        currentPhase = CombatPhase;
        CombatPhase.StartPhase();
    }
}
