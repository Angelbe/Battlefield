public class DeploymentPhase : IBattlePhase
{
    private readonly BattlefieldController bfController;
    private readonly PhaseManager phaseManager;
    private readonly UIDeployController uIDeployController;
    public Army ActiveArmy;

    public DeploymentPhase(BattlefieldController bfController, UIController newUIController, PhaseManager newPhaseManager)
    {
        this.bfController = bfController;
        uIDeployController = newUIController.UIDeployController;
        phaseManager = newPhaseManager;
    }

    public void StartPhase()
    {
        uIDeployController.Init(bfController, this);
        uIDeployController.OnFinishButtonclicked += HandleFinishButtonClicked;
        StartAttackerDeployment();
    }

    public void ExitPhase()
    {
        bfController.BfHighlight.ClearDeploymentZones();
        uIDeployController.OnFinishButtonclicked -= HandleFinishButtonClicked;
        uIDeployController.Shutdown();
        phaseManager.StartCombatPhase();
    }

    public void SetActiveArmy(Army newActiveArmy)
    {
        ActiveArmy = newActiveArmy;
    }


    public void HandleFinishButtonClicked()
    {
        if (ActiveArmy == bfController.bfModel.Attacker)
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
        SetActiveArmy(bfController.bfModel.Attacker);
        phaseManager.FinishDeploymentPhase();
    }

    public void StartAttackerDeployment()
    {
        bfController.BfHighlight.ClearDeploymentZones();
        SetActiveArmy(bfController.bfModel.Attacker);
        bfController.BfSpawn.SetActiveArmy(ActiveArmy);
        bfController.BfHighlight.ShowAttackerDeploymentZone(ActiveArmy.Champion.DeploymentLevel);
        uIDeployController.ShowAttackerDeploy();
    }

    public void StartDefenderDeployment()
    {
        bfController.BfHighlight.ClearDeploymentZones();
        SetActiveArmy(bfController.bfModel.Defender);
        bfController.BfSpawn.SetActiveArmy(ActiveArmy);
        bfController.BfHighlight.ShowDefenderDeploymentZone(ActiveArmy.Champion.DeploymentLevel);
        uIDeployController.ShowDefenderDeploy();
    }

}
