using System.Collections.Generic;
using System.Linq;
// ← no necesitas  using UnityEngine;  en este archivo porque no usas tipos de Unity

public static class DeploymentZone
{
    public static List<CubeCoord> GetZone(bool isAttacker, EDeploymentLevel level)
    {
        // 1 Lista que devolveremos
        var coords = new List<CubeCoord>();

        // --- BASIC ----------------------------------------------------------
        if (level >= EDeploymentLevel.Basic)
            coords.AddRange(CalcRect(-3, 0, 9));          // zona atacante básica

        // --- ADVANCED (y superiores) ---------------------------------------
        if (level >= EDeploymentLevel.Advanced)
        {
            coords.Add(new CubeCoord(0, 0, 0));
            coords.Add(new CubeCoord(-5, -5, 10));       // ejemplo extra
        }

        // — Más niveles aquí …

        // 2 Si el jugador NO es el atacante, espejamos la zona
        if (!isAttacker)
            coords = coords.Select(Mirror).ToList();

        return coords;
    }

    /* *********************************************************************/
    /* Helpers                                                              */
    /* *********************************************************************/

    // Genera un “rectángulo” triangular (inclusive) entre zStart y zEnd
    private static IEnumerable<CubeCoord> CalcRect(int xStart, int zStart, int zEnd)
    {
        int dz = (zEnd >= zStart) ? 1 : -1;
        for (int z = zStart; z != zEnd + dz; z += dz)
        {
            for (int x = xStart; x <= 0; x++)
            {
                int y = -x - z;
                yield return new CubeCoord(x, y, z);
            }
        }
    }

    // Devuelve la coordenada “reflejada” respecto al origen (lado opuesto)
    private static CubeCoord Mirror(CubeCoord c) => new(-c.X, -c.Y, -c.Z);
}
