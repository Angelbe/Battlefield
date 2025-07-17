using System.Collections.Generic;
using System.Linq;

public class Pathfinder
{
    private readonly Dictionary<CubeCoord, TileController> tileControllers;

    public Pathfinder(Dictionary<CubeCoord, TileController> tiles)
    {
        tileControllers = tiles;
    }

    public List<CubeCoord> GetReachableTiles(CubeCoord origin, int range)
    {
        HashSet<CubeCoord> visited = new() { origin };
        Queue<CubeCoord> frontier = new();
        frontier.Enqueue(origin);

        while (frontier.Count > 0)
        {
            CubeCoord current = frontier.Dequeue();

            foreach (var dir in CubeCoord.CubeDirections.Values)
            {
                CubeCoord next = current + dir;

                if (visited.Contains(next)) continue;
                if (!tileControllers.TryGetValue(next, out var tile)) continue;
                if (tile.OccupantCreature != null) continue;
                //Verificar también que en la casilla cabe el shape de la criatura, posiblemente usando el método DoesTheCreatureFitInTile del CreatureValidator class
                //La casilla inicial no es una posición valida

                int distance = CubeCoordDistance(origin, next);
                if (distance <= range)
                {
                    visited.Add(next);
                    frontier.Enqueue(next);
                }
            }
        }

        return visited.ToList();
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

                if (!tileControllers.ContainsKey(next)) continue;
                if (tileControllers[next].OccupantCreature != null && next != goal) continue;

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

