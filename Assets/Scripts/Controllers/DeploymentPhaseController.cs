public class DeploymentPhaseController : IBattlePhase
{
    private readonly BattlefieldController bfController;
    private readonly BattlefieldModel      bfModel;
    private readonly HexHighlightController highlights;
    private readonly PhaseManager          phaseManager;

    public DeploymentPhaseController(
        BattlefieldController bfCtrl,
        HexHighlightController hhCtrl,
        BattlefieldModel      model,
        PhaseManager          pm)
    {
        bfController = bfCtrl;
        highlights   = hhCtrl;
        bfModel      = model;
        phaseManager = pm;
    }

    /* --------- Entrar / Salir --------- */
    public void EnterPhase()
    {
        ShowDeploymentZoneFromArmy(bfModel.ActiveArmy);
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

        if (bfModel.ActiveArmy == bfModel.Attacker)
        {
            bfModel.SetActiveArmy(bfModel.Defender);
            ShowDeploymentZoneFromArmy(bfModel.ActiveArmy);
        }
        else
        {
            phaseManager.ChangePhase(EBattlePhase.Combat);
        }
    }

    private void ShowDeploymentZoneFromArmy(Army army)
    {
        var coords = DeploymentZone.GetZone(army.IsAttacker, army.DeploymentLevel);
        DeploymentZone.PaintZone(coords, bfController);
    }
}
