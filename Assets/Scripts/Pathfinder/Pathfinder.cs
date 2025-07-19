using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder
{
    private readonly BattlefieldController bfController;
    private readonly Dictionary<CubeCoord, TileController> tilesInTheBattlefield;
    private readonly CreatureShapeCatalog shapeCatalog = new();

    public Pathfinder(BattlefieldController newBfController)
    {
        bfController = newBfController;
        tilesInTheBattlefield = newBfController.BfGrid.TilesInTheBattlefield; ;
    }

    public List<CubeCoord> GetReachableTiles(CreatureController creature)
    {
        CubeCoord origin = creature.OccupiedTiles[0].Model.Coord;
        List<CubeCoord> shapeOffsets = shapeCatalog.GetShape(creature.Model.Shape);
        int maxRange = creature.Stats.Speed;

        Dictionary<CubeCoord, int> costSoFar = new();
        PriorityQueue<CubeCoord> frontier = new();
        List<CubeCoord> result = new();

        frontier.Enqueue(origin, 0);
        costSoFar[origin] = 0;

        while (!frontier.IsEmpty)
        {
            CubeCoord current = frontier.Dequeue();
            int currentCost = costSoFar[current];

            foreach (CubeCoord next in current.GetNeighbors())
            {
                if (costSoFar.ContainsKey(next)) continue;
                if (!bfController.BfGrid.DoesTileExist(next)) continue;
                if (!creature.Movement.CanStandOnTile(tilesInTheBattlefield[next])) continue;

                int newCost = currentCost + 1;
                if (newCost > maxRange) continue;

                costSoFar[next] = newCost;
                frontier.Enqueue(next, newCost);
                if (next != origin) result.Add(next);
            }
        }

        return result;
    }


    public List<CubeCoord> GetPath(CubeCoord start, CubeCoord goal)
    {
        Dictionary<CubeCoord, CubeCoord> cameFrom = new();
        Dictionary<CubeCoord, int> costSoFar = new();
        PriorityQueue<CubeCoord> frontier = new();

        frontier.Enqueue(start, 0);
        cameFrom[start] = start;
        costSoFar[start] = 0;

        while (!frontier.IsEmpty)
        {
            CubeCoord current = frontier.Dequeue();

            if (current == goal)
                break;

            foreach (var dir in CubeCoord.CubeDirections.Values)
            {
                CubeCoord next = current + dir;

                if (!tilesInTheBattlefield.ContainsKey(next)) continue;
                if (tilesInTheBattlefield[next].OccupantCreature != null && next != goal) continue;

                int newCost = costSoFar[current] + 1;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    int priority = newCost + CubeCoordDistance(next, goal);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(goal))
            return new(); // sin camino

        List<CubeCoord> path = new();
        CubeCoord currentStep = goal;
        while (currentStep != start)
        {
            path.Add(currentStep);
            currentStep = cameFrom[currentStep];
        }
        path.Reverse();
        return path;
    }


    private int CubeCoordDistance(CubeCoord a, CubeCoord b)
    {
        return (System.Math.Abs(a.X - b.X) + System.Math.Abs(a.Y - b.Y) + System.Math.Abs(a.Z - b.Z)) / 2;
    }
}


public class PriorityQueue<T>
{
    private readonly List<(T item, int priority)> elements = new();

    public int Count => elements.Count;
    public bool IsEmpty => elements.Count == 0;

    public void Enqueue(T item, int priority)
    {
        elements.Add((item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].priority < elements[bestIndex].priority)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].item;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }

    public void Clear()
    {
        elements.Clear();
    }
}

