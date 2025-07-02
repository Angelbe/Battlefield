public class DeploymentPhaseController : IBattlePhase
{
    private readonly BattlefieldController bfController;
    private readonly BattlefieldModel bfModel;
    private readonly HexHighlightController highlights;
    private readonly PhaseManager phaseManager;

    public DeploymentPhaseController(
        BattlefieldController bfCtrl,
        HexHighlightController hhCtrl,
        BattlefieldModel model,
        PhaseManager pm)
    {
        bfController = bfCtrl;
        highlights = hhCtrl;
        bfModel = model;
        phaseManager = pm;
    }

    /* --------- Entrar / Salir --------- */
    public void EnterPhase()
    {
        ShowDeploymentZoneFromArmy(bfController.ActiveArmy, true);
    }

    public void ExitPhase()
    {
        highlights.ClearHighlightsByType(ETileHighlightType.DeployZone);
        // limpiar UI, etc.
    }

    /* --------- Interacciones --------- */
    private void FinishCurrentArmy()
    {
        highlights.ClearHighlightsByType(ETileHighlightType.DeployZone);

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
        var coords = DeploymentZone.GetZone(isAttacker, army.ChampionModel.DeploymentLevel);
        DeploymentZone.PaintZone(coords, bfController);
    }
}
