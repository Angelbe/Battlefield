public class PhaseManager
{
    private IBattlePhase currentPhase;
    private readonly BattlefieldModel bfModel;

    // Guardamos referencias si las necesitas más adelante
    private readonly BattlefieldController bfCtrl;

    public PhaseManager(BattlefieldModel model,
                        BattlefieldController controller)
    {
        this.bfModel = model;
        this.bfCtrl = controller;
    }

    /* ───────────────────────────  arranque ─────────────────────────── */
    public void StartBattle()
    {
        currentPhase = new DeploymentPhaseController(
                           bfCtrl,
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
            EBattlePhase.Deployment => new DeploymentPhaseController(bfCtrl, bfModel, this),
            // EBattlePhase.Combat => new CombatPhaseController(bfCtrl, highlightCtrl, model, this),
            // EBattlePhase.Results => new ResultsPhaseController(bfCtrl, highlightCtrl, model, this),
            _ => currentPhase               // por seguridad
        };

        currentPhase.EnterPhase();
    }
}
