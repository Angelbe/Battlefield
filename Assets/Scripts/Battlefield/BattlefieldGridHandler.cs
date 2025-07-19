using System.Collections.Generic;
using UnityEngine;

public class BattlefieldGridHandler
{
    private BattlefieldConfig bfConfiguration;
    private BattlefieldController bfController;
    public Vector2 Center { get; private set; }
    public Dictionary<CubeCoord, TileController> TilesInTheBattlefield { get; private set; } = new();

    public BattlefieldGridHandler(BattlefieldConfig battlefieldConfigAssigned, BattlefieldController newBattlefieldController)
    {
        bfConfiguration = battlefieldConfigAssigned;
        bfController = newBattlefieldController;
    }

    public Dictionary<CubeCoord, TileModel> GenerateGridModel()
    {
        int rows = bfConfiguration.Rows;
        int cols = bfConfiguration.Cols;
        float size = bfConfiguration.HexSize;
        var result = new Dictionary<CubeCoord, TileModel>();

        for (int row = 0; row < rows; row++)
        {
            int colsInRow = (row & 1) == 0 ? cols : cols - 1;
            for (int col = 0; col < colsInRow; col++)
            {
                ColRow colRow = new(col, row);
                CubeCoord cube = CubeCoord.FromColRow(col, row);
                Vector2 pos = ColRow.FromColRowToWorldPosition(colRow, size);
                var TileModel = new TileModel(cube, pos, colRow, size);
                result[cube] = TileModel;
            }
        }
        return result;
    }

    public void GetGridWorldCenter()
    {
        Vector2 min = Vector2.positiveInfinity;
        Vector2 max = Vector2.negativeInfinity;

        foreach (var tileController in TilesInTheBattlefield.Values)
        {
            Vector2 pos = tileController.transform.position;
            min = Vector2.Min(min, pos);
            max = Vector2.Max(max, pos);
        }

        Center = (min + max) / 2f;
    }

    private void InstantiateTiles(Dictionary<CubeCoord, TileModel> tileModels)
    {

        GameObject GridContainerGO = GameObject.Instantiate(new GameObject("GridContainer"), bfController.transform);
        foreach (TileModel tileModel in tileModels.Values)
        {
            var go = GameObject.Instantiate(bfConfiguration.tilePrefab, tileModel.WorldPosition, Quaternion.identity, GridContainerGO.transform);
            go.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
            var tileController = go.GetComponent<TileController>();
            tileController.Init(tileModel, bfController);
            TilesInTheBattlefield[tileModel.Coord] = tileController;
        }
    }


    public void GenerateGrid()
    {
        Dictionary<CubeCoord, TileModel> tileModels = GenerateGridModel();
        InstantiateTiles(tileModels);
        GetGridWorldCenter();
    }

    public bool DoesTileExist(CubeCoord coord)
    {
        return TilesInTheBattlefield.ContainsKey(coord);
    }

    public TileController GetClosestTileToPosition(Vector3 position, List<TileController> exclusions)
    {
        TileController closest = null;
        float shortestDist = float.MaxValue;

        foreach (TileController tileToCheck in TilesInTheBattlefield.Values)
        {
            TileController tile = tileToCheck;
            if (exclusions.Contains(tile)) continue;

            float dist = (tile.transform.position - position).sqrMagnitude;
            if (dist < shortestDist)
            {
                closest = tile;
                shortestDist = dist;
            }
        }

        return closest;
    }

    public TileController GetClosestTileFromList(Vector3 position, List<TileController> candidates)
    {
        TileController closest = null;
        float shortestDist = float.MaxValue;

        foreach (var tile in candidates)
        {
            float dist = (tile.Model.WorldPosition - position).sqrMagnitude;
            if (dist < shortestDist)
            {
                closest = tile;
                shortestDist = dist;
            }
        }

        return closest;
    }


}