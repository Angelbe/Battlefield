using UnityEngine;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] private BattlefieldConfig battlefieldConfig;

    // Prototipo de datos: luego vendrán de menú, savegame, etc.
    private void Awake()
    {
        var attacker = BuildSampleArmy(isAttacker: true);
        var defender = BuildSampleArmy(isAttacker: false);

        // 1- Crear el modelo y el gestor de fases
        // TODO: crear camara y light
        var bfModel = new BattlefieldModel(attacker, defender);
        var phaseManager = new PhaseManager(bfModel);

        // 2- Instanciar el controlador de escena
        var battlefieldGO = Instantiate(battlefieldConfig.battlefieldPrefab);
        var battlefieldCtrl = battlefieldGO.GetComponent<BattlefieldController>();

        // 3- Inyectar todo ANTES de que arranque el Start() de BattlefieldController
        battlefieldCtrl.Init(bfModel, phaseManager, battlefieldConfig);
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
