using System;

public interface ICreatureStack
{
    public Guid ID { get; }
    public CreatureModel Creature { get; }
    public int Quantity { get; }
    void Add(int amount);
    void Remove(int amount);
}