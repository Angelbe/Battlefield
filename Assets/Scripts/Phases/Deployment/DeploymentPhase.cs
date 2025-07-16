public class DeploymentPhase : IBattlePhase
{
    private readonly BattlefieldController bfController;
    private readonly PhaseManager phaseManager;
    private readonly UIDeployController uIDeployController;

    public DeploymentPhase(
        BattlefieldController bfController,
        UIController newUIController,
        PhaseManager newPhaseManager)
    {
        this.bfController = bfController;
        uIDeployController = newUIController.UIDeployController;
        phaseManager = newPhaseManager;
    }

    public void StartPhase()
    {
        bfController.PaintAttackerDeploymentZone();
        uIDeployController.Init(bfController, this);
        uIDeployController.OnFinishButtonclicked += HandleFinishButtonClicked;
    }

    public void ExitPhase()
    {
        bfController.ClearDeploymentZones();
        uIDeployController.OnFinishButtonclicked -= HandleFinishButtonClicked;
        uIDeployController.Shutdown();
        phaseManager.StartCombatPhase();
    }

    public void HandleFinishButtonClicked()
    {
        if (bfController.ActiveArmy == bfController.bfModel.Attacker)
        {
            HandleAttackerFinishDeploy();
            return;
        }
        HandleDefenderFinishDeploy();
    }

    public void HandleAttackerFinishDeploy()
    {

        StartDefenderDeployment();
    }

    public void HandleDefenderFinishDeploy()
    {
        bfController.SetActiveArmy(bfController.bfModel.Attacker);
        phaseManager.FinishDeploymentPhase();
    }

    public void StartDefenderDeployment()
    {
        bfController.ClearDeploymentZones();
        bfController.SetActiveArmy(bfController.bfModel.Defender);
        bfController.PaintDefenderDeploymentZone();
        bfController.BfSpawn.uIDeployController.ShowDefenderDeploy();
    }

}
