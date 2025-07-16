using System.Collections.Generic;
using UnityEngine;

public interface IArmy
{
    string Name { get; }
    bool IsAttacker { get; }
    public ReserveHandler Reserve { get; }
    public DeployHandler Deployed { get; }
    ChampionModel Champion { get; }
    Color ArmyColor { get; }
}

public class Army : IArmy
{
    public string Name { get; private set; }
    public bool IsAttacker { get; private set; }
    public ChampionModel Champion { get; private set; }
    public ReserveHandler Reserve { get; private set; }
    public DeployHandler Deployed { get; private set; }
    public Color ArmyColor { get; private set; }

    public Army(string newArmyName, Color newArmyColor, ChampionModel ChampionOfTheArmy)
    {
        Name = newArmyName;
        ArmyColor = newArmyColor;
        Champion = ChampionOfTheArmy;
        Reserve = new();
        Deployed = new(Reserve);
    }

    public void AddNewCreatureToTheArmy(CreatureModel creatureToAdd, int quantity)
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
