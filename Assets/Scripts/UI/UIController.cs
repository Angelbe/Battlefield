using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IUIController
{
    public BattlefieldController BattlefieldController { get; }
    public void AssignCamera(Camera camera);
    public void StartUIDeploy();
    public void StopUIDeploy();
    public void Init(BattlefieldController newBattlefieldController);
}

[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour, IUIController
{
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
        UIDeployController.Init(BattlefieldController);
    }

    public void StopUIDeploy()
    {
        UIDeployController.Shutdown();
    }

    public void Init(BattlefieldController newBattlefieldController)
    {
        BattlefieldController = newBattlefieldController;

    }

}