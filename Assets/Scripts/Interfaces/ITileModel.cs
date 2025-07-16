using UnityEngine;

public interface ITileModel
{
    public CubeCoord Coord { get; }
    public Vector2 WorldPosition { get; }
    public ColRow ColRow { get; }
    public float Size { get; }
}