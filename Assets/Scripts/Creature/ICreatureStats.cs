public interface ICreatureStats
{
    ECreatureShape CreatureShape { get; }
    int HealthPoint { get; }
    int Attack { get; }
    int Defense { get; }
    EAttackType AttackType { get; }
    int MinDamage { get; }
    int MaxDamage { get; }
    int OptimalRange { get; }
    int Ammunition { get; }
    int Initiative { get; }
    int Speed { get; }
    EMovementType MovementType { get; }
    int Retaliations { get; }
}
