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

public interface ICreatureRangeModel
{
    public int OptimalRange { get; }
    public int Ammunition { get; }
}