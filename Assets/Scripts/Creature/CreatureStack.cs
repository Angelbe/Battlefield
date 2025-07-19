using System;
using UnityEngine;

public class CreatureStack
{
    public Guid ID { get; private set; } = Guid.NewGuid();
    public Army Army { get; private set; }
    public CreatureModel Creature { get; private set; }
    public int Quantity { get; private set; }
    public bool IsDefender { get; private set; }

    public CreatureStack(CreatureModel creature, Army newArmy, int quantity)
    {
        Creature = creature;
        Quantity = quantity;
        Army = newArmy;
    }

    public void Add(int amount) => Quantity += amount;
    public void Remove(int amount) => Quantity = Mathf.Max(Quantity - amount, 0);
    public void SetAsDefender(bool isDefender)
    {
        IsDefender = isDefender;
    }
}
