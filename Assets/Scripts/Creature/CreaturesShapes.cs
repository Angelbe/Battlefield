using System.Collections.Generic;

public class CreatureShapeCatalog : ICreatureShapeCatalog
{
    private readonly Dictionary<ECreatureShape, List<CubeCoord>> shapes = new()
    {
        [ECreatureShape.Single] = new List<CubeCoord>
        {
            new CubeCoord(0, 0, 0)
        },
        [ECreatureShape.Line] = new List<CubeCoord>
        {
            new CubeCoord(0, 0, 0),
            CubeCoord.CubeDirections["W"]
        }
    };

    public List<CubeCoord> GetShape(ECreatureShape shape)
    {
        if (!shapes.TryGetValue(shape, out var coords))
            throw new KeyNotFoundException($"Shape {shape} no definido en el catálogo.");
        return coords;
    }
}
