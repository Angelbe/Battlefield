using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler
{
    private CreatureController crController;
    private BattlefieldController bfController;
    public Pathfinder Pathfinder { get; private set; }
    private HashSet<CubeCoord> reachableTiles = new();


    public MovementHandler(CreatureController owner, BattlefieldController battlefield, Pathfinder pathfinderInstance)
    {
        crController = owner;
        bfController = battlefield;
        Pathfinder = pathfinderInstance;
    }

    public void SetReachableTiles()
    {
        int range = crController.Stats.Speed;
        CubeCoord origin = crController.Positions[0];

        List<CubeCoord> result = Pathfinder.GetReachableTiles(origin, range);
        reachableTiles = new(result);

        Color movementColor = bfController.BfConfig.GetColor(ETileHighlightType.MovementRange);
        foreach (var coord in result)
        {
            if (bfController.TileControllers.TryGetValue(coord, out var tile))
            {
                tile.Highlight.AddColor(3, movementColor);
            }
        }
    }


    public void MoveAlongPath(List<CubeCoord> path)
    {
        crController.StartCoroutine(MoveCoroutine(path));
    }

    private IEnumerator MoveCoroutine(List<CubeCoord> path)
    {
        foreach (CubeCoord step in path)
        {
            Vector3 targetPos = bfController.TileControllers[step].transform.position;

            while ((crController.transform.position - targetPos).sqrMagnitude > 0.01f)
            {
                crController.transform.position = Vector3.MoveTowards(crController.transform.position, targetPos, 5f * Time.deltaTime);
                yield return null;
            }
        }


        CubeCoord finalCoord = path[^1];
        TileController finalTile = bfController.TileControllers[finalCoord];
        crController.SetNewPosition(finalTile);

        ClearReachableTiles();
        bfController.PhaseManager.CombatPhase.HandleCreatureFinishedTurn();
    }



    public bool IsTileReachable(CubeCoord coord)
    {
        return reachableTiles.Contains(coord);
    }

    public void ClearReachableTiles()
    {
        Color movementColor = bfController.BfConfig.GetColor(ETileHighlightType.MovementRange);
        foreach (var coord in reachableTiles)
        {
            if (bfController.TileControllers.TryGetValue(coord, out var tile))
            {
                tile.Highlight.RemoveColor(3, movementColor);
            }
        }
        reachableTiles.Clear();
    }


}
