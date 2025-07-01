using System.Collections.Generic;
using System.Drawing;

public class Army
{
    public string Name;
    public bool IsAttacker;
    public ChampionModel ChampionModel;
    public Color ArmyColor;
    public Dictionary<int, CreatureModel> Reserve = new();     // slot index → criatura
    public List<CreatureModel> Deployed = new();

    public IEnumerable<CubeCoord> GetDeploymentZone()
    {
        return DeploymentZone.GetZone(IsAttacker, ChampionModel.DeploymentLevel);
    }

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
        ChampionModel = newChampionModel;
    }

    // Lógica principal para añadir
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
}
