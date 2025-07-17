// CubeCoord.cs
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CubeCoord : IEquatable<CubeCoord>
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public CubeCoord(int x, int y, int z)
    {
        if (x + y + z != 0) throw new ArgumentException("Cube coordinate must satisfy x + y + z == 0");
        X = x; Y = y; Z = z;
    }

    public static CubeCoord operator +(CubeCoord a, CubeCoord b)
        => new CubeCoord(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static CubeCoord operator -(CubeCoord a, CubeCoord b)
        => new CubeCoord(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public bool Equals(CubeCoord other) => X == other.X && Y == other.Y && Z == other.Z;

    public override bool Equals(object obj) => obj is CubeCoord other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    public static bool operator ==(CubeCoord left, CubeCoord right) => left.Equals(right);
    public static bool operator !=(CubeCoord left, CubeCoord right) => !left.Equals(right);

    public override string ToString() => $"({X}, {Y}, {Z})";
    public static int Distance(CubeCoord a, CubeCoord b)
    {
        return Mathf.Max(Mathf.Abs(a.X - b.X), Mathf.Abs(a.Y - b.Y), Mathf.Abs(a.Z - b.Z));
    }
    public static CubeCoord FromColRow(int col, int row)
    {
        int x = col - (row - (row & 1)) / 2;
        int z = row;
        int y = -x - z;
        return new CubeCoord(x, y, z);
    }

    public static readonly Dictionary<string, CubeCoord> CubeDirections = new()
    {
        { "E",  new CubeCoord(1, -1, 0) },
        { "SE", new CubeCoord(1, 0, -1) },
        { "SW", new CubeCoord(0, 1, -1) },
        { "W",  new CubeCoord(-1, 1, 0) },
        { "NW", new CubeCoord(-1, 0, 1) },
        { "NE", new CubeCoord(0, -1, 1) },
    };

    public readonly bool IsNeighborFromThisCube(CubeCoord b)
    {
        int dx = Mathf.Abs(X - b.X);
        int dy = Mathf.Abs(Y - b.Y);
        int dz = Mathf.Abs(Z - b.Z);
        return dx + dy + dz == 2;   // distancia cúbica 1
    }
}

//Al utilizar JSON.Utility las variables a leer no pueden tener un { get; set;} porque impiden la lectura ni usar interfaces
[Serializable]
public struct CubeCoordDTO
{
    public int X;
    public int Y;
    public int Z;
    public CubeCoord ToModel() => new(X, Y, Z);
}
