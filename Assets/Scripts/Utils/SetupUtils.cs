using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class SetupHelpers
{
    public static void CreateMainCamera(Vector3 position, int ppu = 32, int refX = 640, int refY = 320)
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
    }

    public static void CreateMainCamera()
    {
        var camGO = new GameObject("Main Camera") { tag = "MainCamera" };

        /* 1️⃣  Camera + posición */
        var cam = camGO.AddComponent<Camera>();
        cam.orthographic = true;
        cam.transform.position = new Vector3(8.5f, 4f, -10f);

        /* 2️⃣  Audio listener (si usas audio 2D/3D) */
        camGO.AddComponent<AudioListener>();

        /* 3️⃣  Pixel-Perfect setup */
        var pp = camGO.AddComponent<PixelPerfectCamera>();
        pp.assetsPPU = 32;
        pp.refResolutionX = 640;
        pp.refResolutionY = 320;
        pp.cropFrame = PixelPerfectCamera.CropFrame.None;
        pp.gridSnapping = PixelPerfectCamera.GridSnapping.None;

#if UNITY_URP
        /* 4️⃣  URP extra data (para stacking, post-FX, etc.) */
        camGO.AddComponent<UniversalAdditionalCameraData>();
#endif
    }

    public static void CreateGlobalLight2D()
    {
        GameObject lightGO = new GameObject("Global Light 2D");
        var light2D = lightGO.AddComponent<Light2D>();
        light2D.lightType = Light2D.LightType.Global;
        light2D.intensity = 1f;
        light2D.color = Color.white;
    }
}
