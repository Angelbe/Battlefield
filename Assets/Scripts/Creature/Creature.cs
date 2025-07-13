using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature
{
    public string Name { get; }
    public ECreatureShape Shape { get; }
    public int HealthPoint { get; }
    public int Attack { get; }
    public int Defense { get; }
    public int MinDamage { get; }
    public int MaxDamage { get; }
    public int Initiative { get; }
    public int Speed { get; }
    public EMovementType MovementType { get; }
    public int Retaliations { get; }
}

public class Creature : ICreature
{
    public string Name { get; protected set; }
    public ECreatureShape Shape { get; protected set; }
    public int HealthPoint { get; protected set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    public int MinDamage { get; protected set; }
    public int MaxDamage { get; protected set; }
    public int Initiative { get; protected set; }
    public int Speed { get; protected set; }
    public EMovementType MovementType { get; protected set; }
    public int Retaliations { get; protected set; }
    public virtual EAttackType AttackType => EAttackType.Melee;

    public Creature(string name, ECreatureShape shape, int healthPoint, int attack, int defense, int minDamage, int maxDamage, int initiative, int speed, EMovementType movementType, int retaliations)
    {
        Name = name;
        Shape = shape;
        HealthPoint = healthPoint;
        Attack = attack;
        Defense = defense;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        Initiative = initiative;
        Speed = speed;
        MovementType = movementType;
        Retaliations = retaliations;
    }


}

public interface ICreatureRange
{
    public int OptimalRange { get; }
    public int Ammunition { get; }
}

public class RangedCreature : Creature, ICreatureRange
{
    public int OptimalRange { get; private set; }
    public int Ammunition { get; private set; }

    public RangedCreature(
        string name,
        ECreatureShape shape,
        int healthPoint,
        int attack,
        int defense,
        int minDamage,
        int maxDamage,
        int initiative,
        int speed,
        EMovementType movementType,
        int retaliations,
        int optimalRange,
        int ammunition)
        : base(name, shape, healthPoint, attack, defense, minDamage, maxDamage, initiative, speed, movementType, retaliations)
    {
        OptimalRange = optimalRange;
        Ammunition = ammunition;
    }

    public override EAttackType AttackType => EAttackType.Range;

}

[System.Serializable]
public class CreatureDTO
{
    public string name;
    public string type;

    public string shape;
    public int healthPoint;
    public int attack;
    public int defense;
    public int minDamage;
    public int maxDamage;
    public int initiative;
    public int speed;

    public string movementType;
    public int retaliations;

    public int optimalRange;
    public int ammunition;

    public Creature ToModel()
    {
        if (!Enum.TryParse<ECreatureShape>(shape, true, out var parsedShape))
        {
            Debug.LogWarning($"❌ Shape no válido: '{shape}'");
            parsedShape = ECreatureShape.Single;
        }

        if (!Enum.TryParse<EMovementType>(movementType, true, out var parsedMovement))
        {
            Debug.LogWarning($"❌ MovementType no válido: '{movementType}'");
            parsedMovement = EMovementType.Walk;
        }

        return type == "Ranged"
            ? new RangedCreature(
                name, parsedShape, healthPoint, attack, defense,
                minDamage, maxDamage, initiative, speed,
                parsedMovement, retaliations, optimalRange, ammunition
            )
            : new Creature(
                name, parsedShape, healthPoint, attack, defense,
                minDamage, maxDamage, initiative, speed,
                parsedMovement, retaliations
            );
    }
}


[Serializable]
public class CreatureCatalogWrapper
{
    public List<CreatureDTO> creatures;
}

