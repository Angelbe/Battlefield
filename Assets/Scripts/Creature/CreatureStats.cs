using UnityEngine;

[System.Serializable]
public class CreatureStats : ICreatureStats
{
    [SerializeField] private ECreatureShape creatureShape;
    [SerializeField] private int healthPoint;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private EAttackType attackType;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int optimalRange;
    [SerializeField] private int ammunition;
    [SerializeField] private int initiative;
    [SerializeField] private int speed;
    [SerializeField] private EMovementType movementType;
    [SerializeField] private int retaliations;
    public ECreatureShape CreatureShape => creatureShape;
    public int HealthPoint => healthPoint;
    public int Attack => attack;
    public int Defense => defense;
    public EAttackType AttackType => attackType;
    public int MinDamage => minDamage;
    public int MaxDamage => maxDamage;
    public int OptimalRange => optimalRange;
    public int Ammunition => ammunition;
    public int Initiative => initiative;
    public int Speed => speed;
    public EMovementType MovementType => movementType;
    public int Retaliations => retaliations;
}
