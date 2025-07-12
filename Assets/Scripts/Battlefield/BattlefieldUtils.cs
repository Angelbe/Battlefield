using System.Collections.Generic;

public class BattlefieldUtils
{
    private IEnumerable<CubeCoord> GetColumnRange(
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