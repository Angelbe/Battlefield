using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler
{
    private CreatureController crController;
    private BattlefieldController bfController;
    private CreatureShapeCatalog shapeCatalog = new();
    public Pathfinder Pathfinder { get; private set; }
    public HashSet<CubeCoord> ReachableTiles { get; private set; } = new();

    public MovementHandler(CreatureController owner, BattlefieldController battlefield, Pathfinder pathfinderInstance)
    {
        crController = owner;
        bfController = battlefield;
        Pathfinder = pathfinderInstance;
    }

    public void SetReachableTiles()
    {
        List<CubeCoord> result = Pathfinder.GetReachableTiles(crController);
        ReachableTiles = new(result);

        Color movementColor = bfController.BfConfig.GetColor(ETileHighlightType.MovementRange);
        foreach (var coord in result)
        {
            if (bfController.TileControllers.TryGetValue(coord, out var tile))
            {
                tile.Highlight.AddColor(3, movementColor);
            }
        }
    }


    public void MoveAlongPath(List<CubeCoord> path, Action onComplete = null)
    {
        crController.StartCoroutine(MoveCoroutine(path, onComplete));
    }

    private IEnumerator MoveCoroutine(List<CubeCoord> path, Action onComplete)
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

        onComplete?.Invoke();
    }




    public bool IsTileReachable(CubeCoord coord)
    {
        return ReachableTiles.Contains(coord);
    }

    public void ClearReachableTiles()
    {
        Color movementColor = bfController.BfConfig.GetColor(ETileHighlightType.MovementRange);
        foreach (var coord in ReachableTiles)
        {
            if (bfController.TileControllers.TryGetValue(coord, out var tile))
            {
                tile.Highlight.RemoveColor(3, movementColor);
            }
        }
        ReachableTiles.Clear();
    }

    public bool CanStandOnTile(TileController originTile)
    {
        CubeCoord origin = originTile.Model.Coord;
        CubeCoord[] shape = shapeCatalog.GetShape(crController.Model.Shape);

        foreach (CubeCoord offset in shape)
        {
            CubeCoord pos = origin + offset;

            if (!bfController.TileControllers.TryGetValue(pos, out TileController tile))
                return false;

            if (tile.OccupantCreature != null)
                return false;
        }

        return true;
    }

}
