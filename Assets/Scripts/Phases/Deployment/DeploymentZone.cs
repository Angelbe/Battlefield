using System.Collections.Generic;
using System.Linq;

public static class DeploymentZone
{
    public static List<CubeCoord> GetZone(bool isAttacker, EDeploymentLevel level)
    {
        // 1 Lista que devolveremos
        var coords = new List<CubeCoord>();

        // --- BASIC ----------------------------------------------------------
        if (level >= EDeploymentLevel.Basic)
        {
            if (isAttacker)
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(-4, -5, 9), 1, zigOnEven: true));
                coords.AddRange(ZigZagColumn(new CubeCoord(-3, -6, 9), 1, zigOnEven: true));
            }
            else
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(13, -22, 9), 1, zigOnEven: false));
                coords.AddRange(ZigZagColumn(new CubeCoord(12, -21, 9), 1, zigOnEven: false));
            }
        }

        // --- ADVANCED ---------------------------------------
        if (level >= EDeploymentLevel.Advanced)
        {
            if (isAttacker)
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(-5, -5, 10), 0, zigOnEven: true));
                coords.AddRange(ZigZagColumn(new CubeCoord(-3, -6, 9), 1, zigOnEven: true));
            }
            else
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(13, -23, 10), 0, zigOnEven: false));
                coords.AddRange(ZigZagColumn(new CubeCoord(12, -21, 9), 1, zigOnEven: false));
            }
        }

        // --- EXPERT ---------------------------------------
        if (level >= EDeploymentLevel.Expert)
        {
            if (isAttacker)
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(-5, -5, 10), 0, zigOnEven: true));
                coords.AddRange(ZigZagColumn(new CubeCoord(-4, -6, 10), 0, zigOnEven: true));
            }
            else
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(13, -23, 10), 0, zigOnEven: false));
                coords.AddRange(ZigZagColumn(new CubeCoord(12, -22, 10), 0, zigOnEven: false));
            }
        }

        // --- Master ---------------------------------------
        if (level >= EDeploymentLevel.Master)
        {
            if (isAttacker)
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(-5, -5, 10), 0, zigOnEven: true));
                coords.AddRange(ZigZagColumn(new CubeCoord(-4, -6, 10), 0, zigOnEven: true));
                coords.AddRange(ZigZagColumn(new CubeCoord(-3, -7, 10), 0, zigOnEven: true));
            }
            else
            {
                coords.AddRange(ZigZagColumn(new CubeCoord(13, -23, 10), 0, zigOnEven: false));
                coords.AddRange(ZigZagColumn(new CubeCoord(12, -22, 10), 0, zigOnEven: false));
                coords.AddRange(ZigZagColumn(new CubeCoord(11, -21, 10), 0, zigOnEven: false));
            }
        }

        return coords;
    }

    public static void PaintZone(List<CubeCoord> coordList, BattlefieldController bfController)
    {
        foreach (var coord in coordList)
        {
            bfController.TileCtrls[coord].SetHighlight(ETileHighlightType.DeployZone, true);
        }
    }

    private static IEnumerable<CubeCoord> ZigZagColumn(
        CubeCoord start, int zEnd, bool zigOnEven, int maxSteps = 200)
    {
        var current = start;
        int stepCnt = 0;

        while (true)
        {
            yield return current;
            if (current.Z == zEnd || ++stepCnt >= maxSteps)
                break;

            bool isEvenRow = (current.Z & 1) == 0;
            bool haceZig = isEvenRow == zigOnEven;

            // 1) si toca zig, desplaza X una columna a la derecha
            int nextX = haceZig ? current.X + 1 : current.X;

            // 2) baja una fila (z - 1) y recalcula Y
            int nextZ = current.Z - 1;
            int nextY = -nextX - nextZ;

            current = new CubeCoord(nextX, nextY, nextZ);
        }
    }
}
