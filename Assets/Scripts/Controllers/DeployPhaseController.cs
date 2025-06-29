public class DeployPhaseController : IBattlePhase
{
    private BattlefieldController bfController;
    private BattlefieldModel bfModel;
    private HexHighlightController highlightsController;
    private PhaseManager phaseManager;

    public void Init(BattlefieldController newBfController, HexHighlightController newHhcontroller, BattlefieldModel newBfModel, PhaseManager newPhaseManager)
    {
        bfController = newBfController;
        bfModel = newBfModel;
        highlightsController = newHhcontroller;
        phaseManager = newPhaseManager;
    }

    /* --------- Entrar --------- */
    public void EnterPhase()
    {
        ShowZone(bfModel.ActiveArmy);
        // TODO: Activar deployment zone para el army atacante
    }

    /* --------- Salir --------- */
    public void ExitPhase()
    {
        // TODO: limpiar tiles y UI
    }

    /* --------- Helpers --------- */
    private void ShowZone(Army army)
        => highlightsController.SetManyHighlights(army.GetDeploymentZone(), ETileHighlightType.DeployZone);

    /// Llamar cuando el jugador pulse “Confirmar despliegue”
    private void FinishCurrentArmy()
    {
        highlightsController.ClearHighlightsByType(ETileHighlightType.DeployZone);

        if (bfModel.ActiveArmy == bfModel.Attacker)
        {
            bfController.ChangeActiveArmy(bfModel.Defender);
            // ShowZone(activeArmy);      // segundo jugador despliega
            // TODO: cambiar panel de reserva
        }
        else
        {
            // ambas fuerzas listas → notificar al PhaseManager para pasar a combate
            phaseManager.ChangePhase(EBattlePhase.Combat);
        }
    }
}
