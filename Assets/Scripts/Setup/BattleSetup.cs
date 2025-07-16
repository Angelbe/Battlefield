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
        PhaseManager phaseMgr = new PhaseManager(bfModel, bfController, UIController);
        bfController.Init(battlefieldConfig, bfModel, setupUtils, creatureCatalog, UIController);
        cameraController.Init(cameraBattlefieldConfig);
        UIController.Init(bfController, phaseMgr);
        cameraController.ChangeCameraPosition(bfController.Center);
        UIController.AssignCamera(camGO.GetComponent<Camera>());

        Creature Bandit = creatureCatalog.GetCreatureData(ECreaturesNames.Bandit);
        Creature Knight = creatureCatalog.GetCreatureData(ECreaturesNames.Knight);
        Creature Mage = creatureCatalog.GetCreatureData(ECreaturesNames.Mage);
        Creature Fighter = creatureCatalog.GetCreatureData(ECreaturesNames.Fighter);
        Creature Archer = creatureCatalog.GetCreatureData(ECreaturesNames.Archer);
        attacker.AddNewCreatureToTheArmy(Bandit, 10);
        defender.AddNewCreatureToTheArmy(Fighter, 8);
        attacker.AddNewCreatureToTheArmy(Knight, 3);
        defender.AddNewCreatureToTheArmy(Mage, 4);
        attacker.AddNewCreatureToTheArmy(Mage, 4);
        defender.AddNewCreatureToTheArmy(Archer, 6);
        attacker.AddNewCreatureToTheArmy(Fighter, 8);
        defender.AddNewCreatureToTheArmy(Bandit, 10);
        attacker.AddNewCreatureToTheArmy(Archer, 6);
        defender.AddNewCreatureToTheArmy(Knight, 3);

        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }


        /* Arranque de la fase inicial */
        phaseMgr.StartBattle();

    }

}
