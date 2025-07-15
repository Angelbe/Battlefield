using System.Collections.Generic;
using UnityEngine;

public enum ECreatureShape
{
    Single,
    Line
}

public interface ICreatureShapeCatalog
{
    CubeCoord[] GetShape(ECreatureShape shape);
}

public class CreatureShapeCatalog : ICreatureShapeCatalog
{
    private readonly Dictionary<ECreatureShape, CubeCoord[]> _shapes = new()
    {
        [ECreatureShape.Single] = new[]
        {
            new CubeCoord(0, 0, 0)
        },
        [ECreatureShape.Line] = new[]
        {
            new CubeCoord(0, 0, 0),
            CubeCoord.CubeDirections["W"]
        }
    };

    public CubeCoord[] GetShape(ECreatureShape shape)
    {
        if (!_shapes.TryGetValue(shape, out var coords))
            throw new KeyNotFoundException($"Shape {shape} no definido en el catálogo.");
        return coords;
    }
}