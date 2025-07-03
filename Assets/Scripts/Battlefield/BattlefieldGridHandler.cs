using System.Collections.Generic;
using UnityEngine;

public interface IBattlefieldGridHandler
{
    public Dictionary<CubeCoord, TileModel> GenerateGridModel(int numberOfRows, int numberOfColumns, float HexSize);
}

public class BattlefieldGridHandler
{
    private BattlefieldConfig bfConfiguration;
    public BattlefieldGridHandler(BattlefieldConfig battlefieldConfigAssigned)
    {
        bfConfiguration = battlefieldConfigAssigned;
    }

    public Vector2 CubeToWorldPosition(CubeCoord cubeCoord, float hexSize)
    {
        CubeToColRow(cubeCoord, out int col, out int row);
        return OffsetToWorld(col, row, hexSize);
    }

    public void CubeToColRow(CubeCoord coord, out int col, out int row)
    {
        row = coord.Z;
        col = coord.X + (row - (row & 1)) / 2;
    }

    public Vector2 OffsetToWorld(int col, int row, float size)
    {
        float width = Mathf.Sqrt(3f) * size;
        float height = 1.5f * size;
        float offsetX = (row % 2 == 0) ? 0 : width / 2f;
        return new Vector2(col * width + offsetX, row * height);
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

}