using System.Collections.Generic;
using System.Linq;

public class TurnHandler
{
    private List<CreatureController> allCreatures;
    private Queue<CreatureController> currentTurnQueue;
    private HashSet<CreatureController> movedThisTurn;
    public int TurnCount { get; private set; } = 1;

    public TurnHandler(DeployHandler attackers, DeployHandler defenders)
    {
        allCreatures = attackers.Deployed.Values.Concat(defenders.Deployed.Values).ToList();
        RecalculateInitiativeOrder();
    }

    public void RecalculateInitiativeOrder()
    {
        var sorted = allCreatures.OrderByDescending(c => c.Stats.Initiative).ToList();
        currentTurnQueue = new Queue<CreatureController>(sorted);
        movedThisTurn = new HashSet<CreatureController>();
    }

    public CreatureController PeekCurrentCreature()
    {
        return currentTurnQueue.Peek();
    }

    public CreatureController GetNextCreature()
    {
        while (currentTurnQueue.Count > 0)
        {
            var next = currentTurnQueue.Dequeue();
            if (!movedThisTurn.Contains(next))
            {
                movedThisTurn.Add(next);
                return next;
            }
        }

        StartNewTurn();
        return GetNextCreature();
    }

    private void StartNewTurn()
    {
        TurnCount++;
        movedThisTurn.Clear();
        RecalculateInitiativeOrder();
    }

    public List<CreatureController> GetNextTurnOrder(int count)
    {
        List<CreatureController> result = new();
        Queue<CreatureController> tempQueue = new(currentTurnQueue);
        HashSet<CreatureController> seen = new(movedThisTurn);

        while (result.Count < count)
        {
            if (tempQueue.Count == 0)
            {
                // Simula próximo turno
                StartNewTurn();
                tempQueue = new(currentTurnQueue);
                seen.Clear();
            }

            CreatureController creature = tempQueue.Dequeue();
            if (!seen.Contains(creature))
            {
                result.Add(creature);
                seen.Add(creature);
            }
        }

        return result;
    }


    public bool HasEveryoneMovedThisTurn() => movedThisTurn.Count >= allCreatures.Count;
}
