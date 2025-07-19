using System.Collections.Generic;

public interface ICreatureShapeCatalog
{
    List<CubeCoord> GetShape(ECreatureShape shape);
}