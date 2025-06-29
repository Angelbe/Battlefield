using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldModel
{
    public int GridHeight = 11;
    public Army Attacker { get; }
    public Army Defender { get; }
    public Army ActiveArmy;


    // Coord → stack desplegado
    private readonly Dictionary<CubeCoord, CreatureModel> deployed = new();

    public event Action<CubeCoord, CreatureModel> OnCreatureDeployed;
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

    /* API de dominio */
    // public bool CanDeployCreature(Army army, CreatureModel creature, CubeCoord target, IEnumerable<CubeCoord> offsets)
    // {
    //     // 1) ¿Está dentro de la zona de army?
    //     // 2) ¿Casillas libres?
    //     // return true / false;
    // }

    public void DeployCreature(Army army, CreatureModel creature, CubeCoord center, IEnumerable<CubeCoord> offsets = null)
    {
        // Actualiza colecciones
        foreach (var coord in creature.OccupiedCoords)
            deployed[coord] = creature;

        army.Reserve.Remove(creature);
        army.Deployed.Add(creature);

        OnCreatureDeployed?.Invoke(center, creature);
    }

    public CreatureModel GetCreatureAt(CubeCoord coord)
        => deployed.TryGetValue(coord, out var c) ? c : null;


}
