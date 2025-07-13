using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Camera), typeof(PixelPerfectCamera), typeof(AudioListener))]
public class CameraBattlefieldController : MonoBehaviour
{
    public void Init(CameraBattlefieldConfig config)
    {
        Camera cam = GetComponent<Camera>();
        cam.orthographic = true;
        transform.position = config.initialPosition;

        PixelPerfectCamera pp = GetComponent<PixelPerfectCamera>();
        pp.assetsPPU = config.assetsPPU;
        pp.refResolutionX = config.refResolutionX;
        pp.refResolutionY = config.refResolutionY;

        gameObject.tag = "MainCamera";
    }

    public void ChangeCameraPosition(Vector2 newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}