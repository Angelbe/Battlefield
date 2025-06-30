using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] private BattlefieldConfig battlefieldConfig;

    // Prototipo de datos: luego vendrán de menú, savegame, etc.
    private void Awake()
    {
        /* 0 – Crear la cámara y la luz antes de hacer nada más */
        CreateMainCamera();
        CreateLight();

        /* 1 – Ejércitos de prueba */
        var attacker = BuildSampleArmy(isAttacker: true);
        var defender = BuildSampleArmy(isAttacker: false);

        /* 2 – Modelo + gestor de fases */
        var bfModel = new BattlefieldModel(attacker, defender);
        var phaseManager = new PhaseManager(bfModel);

        /* 3 – Instanciar el controlador de campo de batalla */
        var battlefieldGO = Instantiate(battlefieldConfig.battlefieldPrefab);
        var battlefieldCtr = battlefieldGO.GetComponent<BattlefieldController>();

        /* 4 – Inyección de dependencias antes de Start() */
        battlefieldCtr.Init(bfModel, phaseManager, battlefieldConfig);
    }

    private void CreateMainCamera()
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

    private void CreateLight()
    {
        GameObject lightGO = new GameObject("Global Light 2D");
        var light2D = lightGO.AddComponent<Light2D>();
        light2D.lightType = Light2D.LightType.Global;
        light2D.intensity = 1f;
        light2D.color = Color.white;
    }

    private Army BuildSampleArmy(bool isAttacker)
    {
        var army = new Army
        {
            Name = isAttacker ? "Attacker" : "Defender",
            IsAttacker = isAttacker,
            DeploymentLevel = EDeploymentLevel.Basic,
        };

        // Aquí meterías criaturas en Reserve (placeholder)
        army.Reserve.Add(new CreatureModel(new CubeCoord(0, 0, 0)));

        return army;
    }
}
