using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BattleSetup : MonoBehaviour
{
    [SerializeField] private GameObject Cursor;
    [SerializeField] private BattlefieldConfig battlefieldConfig;

    // Prototipo de datos: luego vendrán de menú, savegame, etc.
    void Awake()
    {
        SetupHelpers.CreateMainCamera(new Vector3(8.5f, 4, -10));
        SetupHelpers.CreateGlobalLight2D();

        Army attacker = SampleArmy.ArmyOne;
        Army defender = SampleArmy.ArmyTwo;

        BattlefieldModel bfModel = new BattlefieldModel(attacker, defender);

        /* Instancia el prefab del campo de batalla */
        var cursor = Instantiate(Cursor);
        var bfGO = Instantiate(battlefieldConfig.battlefieldPrefab);
        var bfCtrl = bfGO.GetComponent<BattlefieldController>();
        bfCtrl.Init(bfModel, battlefieldConfig);

        /* Necesitas el highlightCtrl después de que el grid se genere */
        bfCtrl.GenerateHexGrid();                                  // o en Start
        var highlightCtrl = new HexHighlightController(bfCtrl.TileCtrls);

        /* PhaseManager con todas las dependencias */
        var phaseMgr = new PhaseManager(bfModel, bfCtrl, highlightCtrl);

        /* Arranque de la fase inicial */
        phaseMgr.StartBattle();
    }

}
