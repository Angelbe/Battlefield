public interface IBattlefieldSpawnController
{
    public void Init(BattlefieldController newBattlefieldController, CreatureCatalog newCreatureCatalog, UIDeployController newUIDeployController, BattlefieldMouseHandler newBFMouseHandler);
    public void Shutdown();
}
