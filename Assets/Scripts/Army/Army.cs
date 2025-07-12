using System.Collections.Generic;
using UnityEngine;

public interface IArmy
{
    string Name { get; }
    bool IsAttacker { get; }
    ChampionModel Champion { get; }
    Color ArmyColor { get; }
    public Dictionary<int, CreatureModel> Reserve { get; }     // slot index → criatura
    public List<CreatureModel> Deployed { get; }
}

public class Army : IArmy
{
    public string Name { get; private set; }
    public bool IsAttacker { get; private set; }
    public ChampionModel Champion { get; private set; } = new("ChampionName");
    public Color ArmyColor { get; private set; }
    public Dictionary<int, CreatureModel> Reserve { get; private set; } = new();     // slot index → criatura
    public List<CreatureModel> Deployed { get; private set; } = new();
    // public IEnumerable<CubeCoord> GetDeploymentZone()
    // {
    //     return DeploymentZone.GetZone(IsAttacker, Champion.DeploymentLevel);
    // }

    public Army(string newArmyName, Color newArmyColor)
    {
        Name = newArmyName;
        ArmyColor = newArmyColor;
    }
    public Army(string newArmyName, Color newArmyColor, Dictionary<int, CreatureModel> newReserve, ChampionModel newChampionModel)
    {
        Name = newArmyName;
        ArmyColor = newArmyColor;
        Reserve = newReserve;
        Champion = newChampionModel;
    }
    public bool TryAddToReserve(CreatureModel creature)
    {
        for (int reserveSlot = 1; reserveSlot <= 10; reserveSlot++)
        {
            if (!Reserve.ContainsKey(reserveSlot))
            {
                Reserve[reserveSlot] = creature;
                return true;
            }
        }
        return false;
    }

    public bool RemoveFromReserve(CreatureModel creature)
    {
        foreach (var ReserveKey in Reserve)
        {
            if (ReserveKey.Value == creature)
            {
                Reserve.Remove(ReserveKey.Key);
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        return Reserve.Count >= 10;
    }

    public void SetIsAttacker(bool isAttacler)
    {
        IsAttacker = isAttacler;
    }
}
