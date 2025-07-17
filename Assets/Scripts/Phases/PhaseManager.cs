using System.Diagnostics;

public class PhaseManager
{
    public IBattlePhase CurrentPhase { get; private set; }
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
        CurrentPhase = DeploymentPhase;
        DeploymentPhase.StartPhase();
    }

    public void FinishDeploymentPhase()
    {
        DeploymentPhase.ExitPhase();
        StartCombatPhase();
    }

    public void StartCombatPhase()
    {
        CurrentPhase = CombatPhase;
        CombatPhase.StartPhase();
    }
}
