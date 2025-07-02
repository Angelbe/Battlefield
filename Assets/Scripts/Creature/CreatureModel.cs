using System;

public class CreatureModel
{
    public string Name { get; }
    public ECreatureShape Shape { get; }

    public int HealthPoint;
    public int Attack;
    public int Defense;
    public EAttackType AttackType;
    public int MinDamage;
    public int MaxDamage;
    public int OptimalRange;
    public int Ammunition;
    public int Initiative;
    public int Speed;
    public EMovementType MovementType;
    public int Retaliations;

    public CreatureModel(
        string name,
        ECreatureShape shape,
        int healthPoint,
        int attack,
        int defense,
        EAttackType attackType,
        int minDamage,
        int maxDamage,
        int optimalRange,
        int ammunition,
        int initiative,
        int speed,
        EMovementType movementType,
        int retaliations
    )
    {
        Name = name;
        Shape = shape;
        HealthPoint = healthPoint;
        Attack = attack;
        Defense = defense;
        AttackType = attackType;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        OptimalRange = optimalRange;
        Ammunition = ammunition;
        Initiative = initiative;
        Speed = speed;
        MovementType = movementType;
        Retaliations = retaliations;
    }
}
