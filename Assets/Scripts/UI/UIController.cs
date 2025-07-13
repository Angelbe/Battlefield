using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour
{
    [Serialize]
    public GameObject UIDeployPrefab;
    public UIDeployController UIDeploy { get; private set; }
    public Canvas Canvas { get; private set; }

    public void AssignCamera(Camera camera)
    {
        Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        Canvas.worldCamera = camera;
    }

    public void StartUIDeploy()
    {
        GameObject UIDeployGO = Instantiate(UIDeployPrefab);
        UIDeploy = UIDeployGO.GetComponent<UIDeployController>();
    }

    public void Init()
    {
        Canvas = GetComponent<Canvas>();
    }

}