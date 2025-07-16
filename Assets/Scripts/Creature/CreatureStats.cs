public class CreatureStats
{
    public int HealthPoint;
    public int Attack;
    public int Defense;
    public int MinDamage;
    public int MaxDamage;
    public int Initiative;
    public int Speed;
    public int Retaliations;

    public CreatureStats(CreatureModel baseModel)
    {
        HealthPoint = baseModel.HealthPoint;
        Attack = baseModel.Attack;
        Defense = baseModel.Defense;
        MinDamage = baseModel.MinDamage;
        MaxDamage = baseModel.MaxDamage;
        Initiative = baseModel.Initiative;
        Speed = baseModel.Speed;
        Retaliations = baseModel.Retaliations;
    }

    public void ModifyStat(ECreatureStat stat, int value)
    {
        switch (stat)
        {
            case ECreatureStat.Health: HealthPoint += value; break;
            case ECreatureStat.Attack: Attack += value; break;
            case ECreatureStat.Defense: Defense += value; break;
            case ECreatureStat.MinDamage: MinDamage += value; break;
            case ECreatureStat.MaxDamage: MaxDamage += value; break;
            case ECreatureStat.Initiative: Initiative += value; break;
            case ECreatureStat.Speed: Speed += value; break;
            case ECreatureStat.Retaliations: Retaliations += value; break;
        }
    }

    public int GetStat(ECreatureStat stat) => stat switch
    {
        ECreatureStat.Health => HealthPoint,
        ECreatureStat.Attack => Attack,
        ECreatureStat.Defense => Defense,
        ECreatureStat.MinDamage => MinDamage,
        ECreatureStat.MaxDamage => MaxDamage,
        ECreatureStat.Initiative => Initiative,
        ECreatureStat.Speed => Speed,
        ECreatureStat.Retaliations => Retaliations,
        _ => 0
    };
}