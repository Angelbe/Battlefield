using UnityEngine;

public interface IArmy
{
    string Name { get; }
    bool IsDefender { get; }
    public ReserveHandler Reserve { get; }
    public DeployHandler Deployed { get; }
    ChampionModel Champion { get; }
    Color ArmyColor { get; }
}