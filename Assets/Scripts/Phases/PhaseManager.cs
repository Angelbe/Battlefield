using System.Diagnostics;

public class PhaseManager
{
    private IBattlePhase currentPhase;
    private readonly DeploymentPhaseController deploymentPhaseController;
    public UIController UIController { get; private set; }
    private readonly BattlefieldModel bfModel;
    private readonly BattlefieldController bfCtrl;

    public PhaseManager(BattlefieldModel model, BattlefieldController controller, UIController newUIController)
    {
        UIController = newUIController;
        bfModel = model;
        bfCtrl = controller;
        deploymentPhaseController = new DeploymentPhaseController(bfCtrl, bfModel, UIController, this);
    }
    public void StartBattle()
    {
        currentPhase = deploymentPhaseController;
        deploymentPhaseController.StartPhase();
    }

    public void StartCombat()
    {
        Debug.Print("Start combat");
    }
}
