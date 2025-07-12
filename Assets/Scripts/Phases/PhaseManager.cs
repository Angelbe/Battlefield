using System.Diagnostics;

public class PhaseManager
{
    private IBattlePhase currentPhase;
    private readonly DeploymentPhaseController deploymentPhaseController;
    private readonly BattlefieldModel bfModel;
    private readonly BattlefieldController bfCtrl;

    public PhaseManager(BattlefieldModel model, BattlefieldController controller)
    {
        bfModel = model;
        bfCtrl = controller;
        deploymentPhaseController = new DeploymentPhaseController(bfCtrl, bfModel, this);
    }
    public void StartBattle()
    {
        currentPhase = deploymentPhaseController;
        currentPhase.EnterPhase();
    }

    public void StartCombat()
    {
        Debug.Print("Start combat");
    }
}
