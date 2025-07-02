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

}
