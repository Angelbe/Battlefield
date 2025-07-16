using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour, IUIController
{
    public PhaseManager PhaseManager { get; private set; }
    public BattlefieldController BattlefieldController { get; private set; }
    public UIDeployController UIDeployController;
    public UICombatController UICombatController;
    public Canvas Canvas;

    public void AssignCamera(Camera camera)
    {
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Canvas.worldCamera = camera;
    }

    public void Init(BattlefieldController newBattlefieldController, PhaseManager newPhaseManager)
    {
        PhaseManager = newPhaseManager;
        BattlefieldController = newBattlefieldController;

    }

}