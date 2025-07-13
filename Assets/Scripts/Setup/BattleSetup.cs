using System;
using UnityEngine;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] private BattlefieldConfig battlefieldConfig;
    [SerializeField] private CreatureCatalog creatureCatalog;
    private SetupHelpers setupUtils;

    // Prototipo de datos: luego vendrán de menú, savegame, etc.
    void Awake()
    {
        setupUtils = new();
        // setupUtils.CreateMainCamera(new Vector3(8.5f, 4, -10));
        setupUtils.CreateGlobalLight2D();

        Army attacker = new("Attacker", Color.red, new(), new("Goku"));
        Army defender = new("Defender", Color.blue, new(), new("Vegeta"));

        BattlefieldModel bfModel = new BattlefieldModel(attacker, defender);

        /* Instancia el prefab del campo de batalla */
        var bfGameObject = Instantiate(battlefieldConfig.battlefieldPrefab);
        var bfController = bfGameObject.GetComponent<BattlefieldController>();
        bfController.Init(battlefieldConfig, bfModel, setupUtils);

        /* PhaseManager con todas las dependencias */
        var phaseMgr = new PhaseManager(bfModel, bfController);
        foreach (Creature creature in creatureCatalog.CreaturesByName.Values)
        {
            attacker.AddNewCreatureToTheArmy(creature, 5);
            defender.AddNewCreatureToTheArmy(creature, 5);
        }

        /* Arranque de la fase inicial */
        phaseMgr.StartBattle();

    }

}
