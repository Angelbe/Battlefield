public class PhaseManager
{
    private IBattlePhase currentPhase;
    private readonly BattlefieldModel bfModel;

    // Guardamos referencias si las necesitas más adelante
    private readonly BattlefieldController bfCtrl;
    private readonly HexHighlightController highlightCtrl;

    public PhaseManager(BattlefieldModel model,
                        BattlefieldController controller,
                        HexHighlightController highlightCtrl)
    {
        this.bfModel = model;
        this.bfCtrl = controller;
        this.highlightCtrl = highlightCtrl;
    }

    /* ───────────────────────────  arranque ─────────────────────────── */
    public void StartBattle()
    {
        currentPhase = new DeploymentPhaseController(
                           bfCtrl,
                           highlightCtrl,
                           bfModel,
                           this);
        currentPhase.EnterPhase();
    }

    /* ───────────────────────────  transición ───────────────────────── */
    public void ChangePhase(EBattlePhase next)
    {
        currentPhase.ExitPhase();

        currentPhase = next switch
        {
            EBattlePhase.Deployment => new DeploymentPhaseController(bfCtrl, highlightCtrl, bfModel, this),
            // EBattlePhase.Combat => new CombatPhaseController(bfCtrl, highlightCtrl, model, this),
            // EBattlePhase.Results => new ResultsPhaseController(bfCtrl, highlightCtrl, model, this),
            _ => currentPhase               // por seguridad
        };

        currentPhase.EnterPhase();
    }
}
