using System.Collections.Generic;

public interface IReserveHandler
{
    public Dictionary<int, CreatureStack> Reserve { get; }     // slot index → criatura
    public bool TryAddToReserve(Creature creature, int quantity);
    public bool RemoveFromReserve(int slotIndex, int quantity);
    public bool RemoveAllFromSlot(int slotIndex);
    public bool AddToStack(int slotIndex, int quantity);
    public bool RemoveFromStack(int slotIndex, int quantity);
}

public class ReserveHandler : IReserveHandler
{
    public Dictionary<int, CreatureStack> Reserve { get; private set; } = new();     // slot index → criatura

    public bool TryAddToReserve(Creature creature, int quantity)
    {
        // ¿Ya existe una pila con esta criatura?
        foreach (var kvp in Reserve)
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
            if (!Reserve.ContainsKey(slot))
            {
                Reserve[slot] = new CreatureStack(creature, quantity);
                return true;
            }
        }

        return false;
    }

    public bool RemoveFromReserve(int slotIndex, int quantity)
    {
        if (!Reserve.TryGetValue(slotIndex, out var stack)) return false;

        stack.Remove(quantity);

        if (stack.Quantity <= 0)
        {
            Reserve.Remove(slotIndex);
        }

        return true;
    }

    public bool RemoveAllFromSlot(int slotIndex)
    {
        return RemoveFromReserve(slotIndex, int.MaxValue);
    }

    public bool AddToStack(int slotIndex, int quantity)
    {
        if (!Reserve.TryGetValue(slotIndex, out CreatureStack stack)) return false;

        stack.Add(quantity);
        return true;
    }

    public bool RemoveFromStack(int slotIndex, int quantity)
    {
        if (!Reserve.TryGetValue(slotIndex, out CreatureStack stack)) return false;

        stack.Remove(quantity);

        if (stack.Quantity <= 0)
        {
            Reserve.Remove(slotIndex);
        }

        return true;
    }
}