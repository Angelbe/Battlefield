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

    /* --------- Entrar / Salir --------- */
    public void EnterPhase()
    {
        ShowDeploymentZoneFromArmy(bfController.ActiveArmy, false);
    }

    public void ExitPhase()
    {
        // highlights.ClearHighlightsByType(ETileHighlightType.DeployZone);
        // limpiar UI, etc.
    }

    /* --------- Interacciones --------- */
    private void FinishCurrentArmy()
    {
        // highlights.ClearHighlightsByType(ETileHighlightType.DeployZone);

        if (bfController.ActiveArmy == bfModel.Attacker)
        {
            bfController.SetActiveArmy(bfModel.Defender);
            ShowDeploymentZoneFromArmy(bfController.ActiveArmy, false);
        }
        else
        {
            phaseManager.ChangePhase(EBattlePhase.Combat);
        }
    }

    private void ShowDeploymentZoneFromArmy(Army army, bool isAttacker)
    {
        var coords = deploymentZone.GetZone(isAttacker, army.Champion.DeploymentLevel);
        bfController.PaintManyTiles(coords, ETileHighlightType.DeployZone);
    }
}
