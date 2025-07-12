public class DeploymentPhaseController : IBattlePhase
{
    private readonly DeploymentZone deploymentZone;
    private readonly BattlefieldController bfController;
    private readonly BattlefieldModel bfModel;
    private readonly TileController highlights;
    private readonly PhaseManager phaseManager;

    public DeploymentPhaseController(
        BattlefieldController bfCtrl,
        BattlefieldModel model,
        PhaseManager pm)
    {
        bfController = bfCtrl;
        bfModel = model;
        phaseManager = pm;
    }

    public void EnterPhase()
    {
        bfController.PaintAttackerDeploymentZone();
    }

    public void ExitPhase()
    {
        bfController.ClearDeploymentZones();
        phaseManager.ChangePhase(EBattlePhase.Combat);

    }

    public void StartDefenderDeployment()
    {
        bfController.ClearDeploymentZones();
        bfController.SetActiveArmy(bfModel.Defender);
        bfController.PaintDefenderDeploymentZone();
    }

}
