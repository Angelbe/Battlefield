
using UnityEngine;

public class TileModel : ITileModel
{
    public CubeCoord Coord { get; private set; }
    public Vector2 WorldPosition { get; private set; }
    public ColRow ColRow { get; private set; }
    public float Size { get; private set; }
    public TileModel(CubeCoord cubeCoord, Vector2 worldPosition, ColRow colRowAssigned, float sizeAssigned)
    {
        Coord = cubeCoord;
        WorldPosition = worldPosition;
        ColRow = colRowAssigned;
        Size = sizeAssigned;
    }
}
