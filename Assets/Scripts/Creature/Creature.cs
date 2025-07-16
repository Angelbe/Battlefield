using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatureModel
{
    public ECreaturesNames Name { get; }
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

public class CreatureModel : ICreatureModel
{
    public ECreaturesNames Name { get; protected set; }
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

    public CreatureModel(ECreaturesNames name, ECreatureShape shape, int healthPoint, int attack, int defense, int minDamage, int maxDamage, int initiative, int speed, EMovementType movementType, int retaliations)
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

public interface ICreatureRangeModel
{
    public int OptimalRange { get; }
    public int Ammunition { get; }
}

public class RangedCreatureModel : CreatureModel, ICreatureRangeModel
{
    public int OptimalRange { get; private set; }
    public int Ammunition { get; private set; }

    public RangedCreatureModel(
        ECreaturesNames name,
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

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public CreatureModel? ToModel()
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
        if (!Enum.TryParse<ECreaturesNames>(name, true, out var parsedName))
        {
            Debug.LogWarning($"❌ Nombre inválido en JSON: '{name}' (no coincide con enum)");
            return null;
        }

        if (!Enum.TryParse<ECreatureShape>(shape, true, out var parsedShape))
        {
            Debug.LogWarning($"❌ Shape inválido: '{shape}' de la criatura {name}'");
            return null;
        }

        if (!Enum.TryParse<EMovementType>(movementType, true, out var parsedMovement))
        {
            Debug.LogWarning($"❌ MovementType inválido: '{movementType}' de la criatura {name}'");
            return null;
        }

        return type == "Ranged"
            ? new RangedCreatureModel(
                parsedName, parsedShape, healthPoint, attack, defense,
                minDamage, maxDamage, initiative, speed,
                parsedMovement, retaliations, optimalRange, ammunition
            )
            : new CreatureModel(
                parsedName, parsedShape, healthPoint, attack, defense,
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

