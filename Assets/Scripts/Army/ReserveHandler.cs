using System.Collections.Generic;

public interface IReserveHandler
{
    public Dictionary<int, CreatureStack> CreaturesInReserve { get; }     // slot index → criatura
    public bool TryAddToReserve(Creature creature, int quantity);
    public bool RemoveFromReserve(int slotIndex, int quantity);
    public bool RemoveAllFromSlot(int slotIndex);
    public bool AddToStack(int slotIndex, int quantity);
    public bool RemoveFromStack(int slotIndex, int quantity);
}

public class ReserveHandler : IReserveHandler
{
    public Dictionary<int, CreatureStack> CreaturesInReserve { get; private set; } = new();     // slot index → criatura

    public bool TryAddToReserve(Creature creature, int quantity)
    {
        if (creature == null) return false;
        // ¿Ya existe una pila con esta criatura?
        foreach (var kvp in CreaturesInReserve)
        {
            if (kvp.Value.Creature.Name == creature.Name)
            {
                kvp.Value.Add(quantity);
                return true;
            }
        }

        // Si hay espacio, agrega en slot nuevo
        for (int slot = 1; slot <= 10; slot++)
        {
            if (!CreaturesInReserve.ContainsKey(slot))
            {
                CreaturesInReserve[slot] = new CreatureStack(creature, quantity);
                return true;
            }
        }

        return false;
    }

    public bool RemoveFromReserve(int slotIndex, int quantity)
    {
        if (!CreaturesInReserve.TryGetValue(slotIndex, out var stack)) return false;

        stack.Remove(quantity);

        if (stack.Quantity <= 0)
        {
            CreaturesInReserve.Remove(slotIndex);
        }

        return true;
    }

    public bool RemoveAllFromSlot(int slotIndex)
    {
        return RemoveFromReserve(slotIndex, int.MaxValue);
    }

    public bool AddToStack(int slotIndex, int quantity)
    {
        if (!CreaturesInReserve.TryGetValue(slotIndex, out CreatureStack stack)) return false;

        stack.Add(quantity);
        return true;
    }

    public bool RemoveFromStack(int slotIndex, int quantity)
    {
        if (!CreaturesInReserve.TryGetValue(slotIndex, out CreatureStack stack)) return false;

        stack.Remove(quantity);

        if (stack.Quantity <= 0)
        {
            CreaturesInReserve.Remove(slotIndex);
        }

        return true;
    }
}