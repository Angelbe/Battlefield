public class PhaseManager
{
    private IBattlePhase currentPhase;
    private readonly BattlefieldModel bfModel;

    private readonly BattlefieldController bfCtrl;

    public PhaseManager(BattlefieldModel model,
                        BattlefieldController controller)
    {
        bfModel = model;
        bfCtrl = controller;
    }
    public void StartBattle()
    {
        currentPhase = new DeploymentPhaseController(
                           bfCtrl,
                           bfModel,
                           this);
        currentPhase.EnterPhase();
    }
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
