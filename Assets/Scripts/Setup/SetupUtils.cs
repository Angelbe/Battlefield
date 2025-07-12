using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SetupHelpers
{
    public GameObject CreateMainCamera(Vector3 position, float PosX = 0, float PosY = 0, int ppu = 32, int refX = 640, int refY = 360)
    {
        var camGO = new GameObject("Main Camera") { tag = "MainCamera" };

        var cam = camGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.transform.position = position;

        camGO.AddComponent<AudioListener>();

        var pp = camGO.AddComponent<PixelPerfectCamera>();
        pp.assetsPPU = ppu;
        pp.refResolutionX = refX;
        pp.refResolutionY = refY;
        pp.cropFrame = PixelPerfectCamera.CropFrame.None;
        pp.gridSnapping = PixelPerfectCamera.GridSnapping.None;

#if UNITY_URP
        camGO.AddComponent<UniversalAdditionalCameraData>();
#endif
        return camGO;
    }

    public void CreateGlobalLight2D()
    {
        GameObject lightGO = new GameObject("Global Light 2D");
        var light2D = lightGO.AddComponent<Light2D>();
        light2D.lightType = Light2D.LightType.Global;
        light2D.intensity = 1f;
        light2D.color = Color.white;
    }
}
