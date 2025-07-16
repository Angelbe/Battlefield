using System;
using UnityEngine;

public interface ICreatureStack
{
    public Guid ID { get; }
    public CreatureModel Creature { get; }
    public int Quantity { get; }
    void Add(int amount);
    void Remove(int amount);
}

public class CreatureStack
{
    public Guid ID { get; private set; } = Guid.NewGuid();
    public CreatureModel Creature { get; private set; }
    public int Quantity { get; private set; }

    public CreatureStack(CreatureModel creature, int quantity)
    {
        Creature = creature;
        Quantity = quantity;
    }

    public void Add(int amount) => Quantity += amount;
    public void Remove(int amount) => Quantity = Mathf.Max(Quantity - amount, 0);
}
