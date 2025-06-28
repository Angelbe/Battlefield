// UnitModel.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class UnitModel
{
    public CubeCoord Center { get; private set; }
    public CubeCoord[] Offsets { get; }
    public IEnumerable<CubeCoord> OccupiedCoords =>
        Offsets.Select(o => Center + o);
    public event Action<CubeCoord> OnPositionChanged;    // ← aquí

    public UnitModel(CubeCoord center, CubeCoord[] offsets = null)
    {
        Center = center;
        Offsets = (offsets == null || offsets.Length == 0)
            ? new CubeCoord[] { new CubeCoord(0, 0, 0) }   // ← ocupa solo el centro
            : offsets;
    }


    public void SetCenter(CubeCoord newCenter)
    {
        if (Center == newCenter) return;
        Center = newCenter;
        OnPositionChanged?.Invoke(newCenter);            // ← dispara evento
    }
}
