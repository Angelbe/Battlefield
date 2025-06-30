using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] private BattlefieldConfig battlefieldConfig;

    // Prototipo de datos: luego vendrán de menú, savegame, etc.
    private void Awake()
    {
        /* 0 – Crear la cámara y la luz antes de hacer nada más */
        SetupHelpers.CreateMainCamera();
        SetupHelpers.CreateGlobalLight2D();

        /* 1 – Ejércitos de prueba */
        var attacker = SetupHelpers.BuildSampleArmy(isAttacker: true, EDeploymentLevel.Basic);
        var defender = SetupHelpers.BuildSampleArmy(isAttacker: false, EDeploymentLevel.Advanced);

        /* 2 – Modelo + gestor de fases */
        var bfModel = new BattlefieldModel(attacker, defender);
        var phaseManager = new PhaseManager(bfModel);

        /* 3 – Instanciar el controlador de campo de batalla */
        var battlefieldGO = Instantiate(battlefieldConfig.battlefieldPrefab);
        var battlefieldCtr = battlefieldGO.GetComponent<BattlefieldController>();

        /* 4 – Inyección de dependencias antes de Start() */
        battlefieldCtr.Init(bfModel, phaseManager, battlefieldConfig);
    }

}
