using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldModel
{
    public int GridHeight = 11;
    public Army Attacker { get; }
    public Army Defender { get; }
    public Army ActiveArmy;
    private readonly Dictionary<CubeCoord, CreatureModel> deployed = new();
    public event Action<CubeCoord> OnCreatureRemoved;

    public BattlefieldModel(Army attacker, Army defender)
    {
        Attacker = attacker;
        Defender = defender;
        ActiveArmy = attacker;
    }

    public void SetActiveArmy(Army newActiveArmy)
    {
        ActiveArmy = newActiveArmy;
    }

    public CreatureModel GetCreatureAt(CubeCoord coord)
        => deployed.TryGetValue(coord, out var c) ? c : null;


}
