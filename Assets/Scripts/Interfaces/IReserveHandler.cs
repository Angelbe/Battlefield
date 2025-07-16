using System.Collections.Generic;

public interface IReserveHandler
{
    public Dictionary<int, CreatureStack> CreaturesInReserve { get; }     // slot index → criatura
    public bool TryAddToReserve(CreatureModel creature, int quantity);
    public bool RemoveFromReserve(int slotIndex, int quantity);
    public bool RemoveAllFromSlot(int slotIndex);
    public bool AddToStack(int slotIndex, int quantity);
    public bool RemoveFromStack(int slotIndex, int quantity);
}