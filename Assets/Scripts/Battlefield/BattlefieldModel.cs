using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldModel
{
    public int GridHeight = 11;
    public Army Attacker { get; private set; }
    public Army Defender { get; private set; }

    public BattlefieldModel(Army attacker, Army defender)
    {
        Attacker = attacker;
        Defender = defender;
        Defender.SetIsDefender(true);
    }

}
