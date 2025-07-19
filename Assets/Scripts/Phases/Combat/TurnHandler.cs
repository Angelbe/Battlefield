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
            CreatureController NextCreature = currentTurnQueue.Dequeue();
            if (!movedThisTurn.Contains(NextCreature))
            {
                movedThisTurn.Add(NextCreature);
                return NextCreature;
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
        HashSet<CreatureController> simulatedMoved = new(movedThisTurn);

        while (result.Count < count)
        {
            if (tempQueue.Count == 0)
            {
                // Simular próximo turno sin modificar el real
                var nextRound = allCreatures
                    .Where(c => !c.isDead)
                    .OrderByDescending(c => c.Stats.Initiative)
                    .ToList();

                tempQueue = new Queue<CreatureController>(nextRound);
                simulatedMoved.Clear();
            }

            if (tempQueue.Count == 0) break;

            var next = tempQueue.Dequeue();
            if (!simulatedMoved.Contains(next))
            {
                result.Add(next);
                simulatedMoved.Add(next);
            }
        }

        return result;
    }

    public void RemoveCreature(CreatureController creature)
    {
        allCreatures.Remove(creature);
        movedThisTurn.Remove(creature);

        Queue<CreatureController> newQueue = new();
        foreach (var c in currentTurnQueue)
            if (c != creature) newQueue.Enqueue(c);
        currentTurnQueue = newQueue;
    }


    public bool HasEveryoneMovedThisTurn() => movedThisTurn.Count >= allCreatures.Count;
}
