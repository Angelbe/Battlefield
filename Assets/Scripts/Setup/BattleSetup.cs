using System;
using System.Numerics;
using UnityEngine;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] private BattlefieldConfig battlefieldConfig;
    [SerializeField] private CameraBattlefieldConfig cameraBattlefieldConfig;
    [SerializeField] private GameObject UIPrefab;
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

        /* Instancia los prefabs del campo de batalla */
        GameObject bfGO = Instantiate(battlefieldConfig.battlefieldPrefab);
        bfGO.name = "Battlefield";
        GameObject camGO = Instantiate(cameraBattlefieldConfig.CameraPrefab);
        camGO.name = "CameraBattlefield";
        GameObject UIGO = Instantiate(UIPrefab);
        UIGO.name = "UI";


        /* Inicializa los controllers*/
        BattlefieldController bfController = bfGO.GetComponent<BattlefieldController>();
        CameraBattlefieldController cameraController = camGO.GetComponent<CameraBattlefieldController>();
        UIController UIController = UIGO.GetComponent<UIController>();
        bfController.Init(battlefieldConfig, bfModel, setupUtils);
        cameraController.Init(cameraBattlefieldConfig);
        cameraController.ChangeCameraPosition(bfController.Center);
        UIController.Init();
        UIController.AssignCamera(camGO.GetComponent<Camera>());

        /* PhaseManager con todas las dependencias */
        var phaseMgr = new PhaseManager(bfModel, bfController, UIController);
        foreach (Creature creature in creatureCatalog.CreaturesByName.Values)
        {
            attacker.AddNewCreatureToTheArmy(creature, 5);
            defender.AddNewCreatureToTheArmy(creature, 5);
        }

        /* Arranque de la fase inicial */
        phaseMgr.StartBattle();

    }

}
