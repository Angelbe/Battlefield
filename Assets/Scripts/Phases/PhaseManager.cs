using System.Diagnostics;

public class PhaseManager
{
    private IBattlePhase currentPhase;
    public readonly DeploymentPhase DeploymentPhase;
    public readonly CombatPhase CombatPhase;
    public UIController UIController { get; private set; }
    private readonly BattlefieldController bfCtrl;

    public PhaseManager(BattlefieldController controller, UIController newUIController)
    {
        UIController = newUIController;
        bfCtrl = controller;
        DeploymentPhase = new DeploymentPhase(bfCtrl, UIController, this);
        CombatPhase = new CombatPhase(bfCtrl, UIController);
    }
    public void StartBattle()
    {
        CombatPhase.ExitPhase();
        currentPhase = DeploymentPhase;
        currentPhase.StartPhase();
    }

    public void FinishDeploymentPhase()
    {
        currentPhase.ExitPhase();
        StartCombatPhase();
    }

    public void StartCombatPhase()
    {
        currentPhase = CombatPhase;
        currentPhase.StartPhase();
    }
}
