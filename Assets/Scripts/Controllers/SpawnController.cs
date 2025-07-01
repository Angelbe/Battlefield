using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private readonly BattlefieldController bfCtrl;
    private readonly CreatureCatalog creatureCatalog;
    public readonly Dictionary<CreatureModel, CreatureView> CreatureViews = new();

    public SpawnController(BattlefieldController bfController, CreatureCatalog creatureCatalog)
    {
        bfCtrl = bfController;
        this.creatureCatalog = creatureCatalog;
    }

    public bool Spawn(CreatureModel creatureModel, CubeCoord anchor, Army armyOwner)
    {
        // 1) comprobar casillas libres
        foreach (var rel in creatureModel.Shape)
        {
            var abs = anchor + rel;
            if (!bfCtrl.TileCtrls.ContainsKey(abs) ||
                bfCtrl.TileCtrls[abs].Model.IsOccupied)
                return false;
        }

        // 2) instanciar prefab
        var prefab = creatureCatalog.GetPrefab(creatureModel.Name);          // variant correcto
        var pos = bfCtrl.WorldPosOf(anchor) + Vector3.up * .01f;
        var go = Object.Instantiate(prefab, pos, Quaternion.identity, bfCtrl.transform);

        // 3) inicializar componentes
        var creatureCtrl = go.GetComponent<CreatureController>();
        creatureCtrl.Init(creatureModel, anchor);

        // 4) registrar en tablero
        // foreach (var c in ctrl.CreaturePositions)
        //     bfCtrl.TileCtrls[c].Model.SetOccupant(model);

        // bfCtrl.Spawner.CreatureViews[model] = view;  // opcional
        // armyOwner.Deployed.Add(model);               // mueve de reserva si procede

        return true;
    }

}
