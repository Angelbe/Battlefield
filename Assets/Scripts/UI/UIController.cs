using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour
{
    [Serialize]
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
        UIDeployController.EnableUI();
        UIDeployController.Init(BattlefieldController);
    }

    public void StopUIDeploy()
    {
        UIDeployController.DisableUI();
    }

    public void Init(BattlefieldController battlefieldController)
    {
        BattlefieldController = battlefieldController;
    }

}