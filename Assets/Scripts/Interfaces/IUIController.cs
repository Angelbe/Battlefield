using UnityEngine;

public interface IUIController
{
    public PhaseManager PhaseManager { get; }
    public BattlefieldController BattlefieldController { get; }
    public void AssignCamera(Camera camera);
    public void Init(BattlefieldController newBattlefieldController, PhaseManager newPhaseManager);
}