using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] private BattlefieldConfig battlefieldConfig;
    private SetupHelpers setupUtils;

    // Prototipo de datos: luego vendrán de menú, savegame, etc.
    void Awake()
    {
        setupUtils = new();
        setupUtils.CreateMainCamera(new Vector3(8.5f, 4, -10));
        setupUtils.CreateGlobalLight2D();

        Army attacker = new("Attacker", Color.red);
        attacker.SetIsAttacker(true);
        Army defender = new("Defender", Color.blue);

        BattlefieldModel bfModel = new BattlefieldModel(attacker, defender);

        /* Instancia el prefab del campo de batalla */
        var bfGameObject = Instantiate(battlefieldConfig.battlefieldPrefab);
        var bfController = bfGameObject.GetComponent<BattlefieldController>();
        bfController.Init(battlefieldConfig, bfModel);

        /* PhaseManager con todas las dependencias */
        var phaseMgr = new PhaseManager(bfModel, bfController);

        /* Arranque de la fase inicial */
        phaseMgr.StartBattle();
    }

}
