using System.Collections.Generic;
using UnityEngine;

public interface IArmy
{
    string Name { get; }
    bool IsAttacker { get; }
    ChampionModel Champion { get; }
    Color ArmyColor { get; }
    public List<CreatureStack> Deployed { get; }
}

public class Army : IArmy
{
    public string Name { get; private set; }
    public bool IsAttacker { get; private set; }
    public ChampionModel Champion { get; private set; } = new("ChampionName");
    public ReserveHandler Reserve { get; private set; } = new();
    public List<CreatureStack> Deployed { get; private set; } = new();
    public Color ArmyColor { get; private set; }

    public Army(string newArmyName, Color newArmyColor)
    {
        Name = newArmyName;
        ArmyColor = newArmyColor;
    }
    public Army(string newArmyName, Color newArmyColor, Dictionary<int, CreatureStack> newReserve, ChampionModel newChampionModel)
    {
        Name = newArmyName;
        ArmyColor = newArmyColor;
        Champion = newChampionModel;
    }

    public void AddNewCreatureToTheArmy(Creature creatureToAdd, int quantity)
    {
        Reserve.TryAddToReserve(creatureToAdd, quantity);
    }

    public void RemoveCreaturesFromSlotReserve(int slot, int quantity)
    {
        Reserve.RemoveFromReserve(slot, quantity);
    }

    public void SetIsAttacker(bool isAttacler)
    {
        IsAttacker = isAttacler;
    }
}
