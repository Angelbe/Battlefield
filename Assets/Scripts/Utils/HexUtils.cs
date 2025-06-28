using UnityEngine;


public static class HexUtils
{
    public static Vector2 OffsetToWorld(int col, int row, float size)
    {
        float width = Mathf.Sqrt(3f) * size;
        float height = 1.5f * size;
        float offsetX = (row % 2 == 0) ? 0 : width / 2f;
        return new Vector2(col * width + offsetX, row * height);
    }

    public static void CubeToOffset(CubeCoord cube, out int col, out int row)
    {
        row = cube.Z;
        col = cube.X + (row - (row & 1)) / 2;
    }

    public static Vector2 CubeToWorld(CubeCoord cube, float size)
    {
        CubeToOffset(cube, out int col, out int row);
        return OffsetToWorld(col, row, size);
    }

    public static bool IsNeighbor(CubeCoord a, CubeCoord b)
    {
        int dx = Mathf.Abs(a.X - b.X);
        int dy = Mathf.Abs(a.Y - b.Y);
        int dz = Mathf.Abs(a.Z - b.Z);
        return dx + dy + dz == 2;   // distancia cúbica 1
    }

    public static readonly CubeCoord[] CubeDirections =
    {
        new CubeCoord(1, -1, 0),
        new CubeCoord(1, 0, -1),
        new CubeCoord(0, 1, -1),
        new CubeCoord(-1, 1, 0),
        new CubeCoord(-1, 0, 1),
        new CubeCoord(0, -1, 1),
    };
}