using UnityEngine;

public interface ITileModel
{
    public CubeCoord Coord { get; }
    public Vector3 WorldPosition { get; }
    public ColRow ColRow { get; }
    public float Size { get; }
}