public class DeploymentPhaseController : IBattlePhase
{
    private readonly BattlefieldController bfController;
    private readonly BattlefieldModel bfModel;
    private readonly PhaseManager phaseManager;
    private readonly UIController uIController;

    public DeploymentPhaseController(
        BattlefieldController bfController,
        BattlefieldModel bfModel,
        UIController newUIController,
        PhaseManager newPhaseManager)
    {
        this.bfController = bfController;
        this.bfModel = bfModel;
        uIController = newUIController;
        phaseManager = newPhaseManager;
    }

    public void StartPhase()
    {
        bfController.PaintAttackerDeploymentZone();
        uIController.StartUIDeploy();
    }

    public void ExitPhase()
    {
        bfController.ClearDeploymentZones();
        uIController.StopUIDeploy();
        phaseManager.StartCombat();
    }

    public void StartDefenderDeployment()
    {
        bfController.ClearDeploymentZones();
        bfController.SetActiveArmy(bfModel.Defender);
        bfController.PaintDefenderDeploymentZone();
        bfController.BfSpawn.uIDeployController.ShowDefenderDeploy();
    }

}
