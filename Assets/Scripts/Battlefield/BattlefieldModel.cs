using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldModel
{
    public int GridHeight = 11;
    public Army Attacker { get; }
    public Army Defender { get; }

    public BattlefieldModel(Army attacker, Army defender)
    {
        Attacker = attacker;
        Defender = defender;
    }


}
