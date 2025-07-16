using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IUIController
{
    public PhaseManager PhaseManager { get; }
    public BattlefieldController BattlefieldController { get; }
    public void AssignCamera(Camera camera);
    public void StartUIDeploy();
    public void StopUIDeploy();
    public void Init(BattlefieldController newBattlefieldController, PhaseManager newPhaseManager);
}

[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour, IUIController
{
    public PhaseManager PhaseManager { get; private set; }
    public BattlefieldController BattlefieldController { get; private set; }
    public UIDeployController UIDeployController;
    public Canvas Canvas;

    public void AssignCamera(Camera camera)
    {
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Canvas.worldCamera = camera;
    }

    public void StartUIDeploy()
    {
        UIDeployController.Init(BattlefieldController, PhaseManager.deploymentPhaseController);
    }

    public void StopUIDeploy()
    {
        UIDeployController.Shutdown();
    }

    public void Init(BattlefieldController newBattlefieldController, PhaseManager newPhaseManager)
    {
        PhaseManager = newPhaseManager;
        BattlefieldController = newBattlefieldController;

    }

}