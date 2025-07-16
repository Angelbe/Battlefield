using System.Collections.Generic;

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