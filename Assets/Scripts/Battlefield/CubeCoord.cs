// CubeCoord.cs
using System;

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

    /* ---------- Operadores aritméticos ---------- */
    public static CubeCoord operator +(CubeCoord a, CubeCoord b)
        => new CubeCoord(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static CubeCoord operator -(CubeCoord a, CubeCoord b)
        => new CubeCoord(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    /* ---------- Igualdad (=> usable en diccionarios) ---------- */
    public bool Equals(CubeCoord other) => X == other.X && Y == other.Y && Z == other.Z;

    public override bool Equals(object obj) => obj is CubeCoord other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    public static bool operator ==(CubeCoord left, CubeCoord right) => left.Equals(right);
    public static bool operator !=(CubeCoord left, CubeCoord right) => !left.Equals(right);

    /* ---------- Conversión a string (debug) ---------- */
    public override string ToString() => $"({X}, {Y}, {Z})";

    public static CubeCoord FromColRow(int col, int row)
    {
        int x = col - (row - (row & 1)) / 2;
        int z = row;
        int y = -x - z;
        return new CubeCoord(x, y, z);
    }
}


public interface ICubeCoordDTO
{
    int X { get; set; }
    int Y { get; set; }
    int Z { get; set; }
    CubeCoord ToModel() => new CubeCoord(X, Y, Z);
}
[Serializable]
public struct CubeCoordDTO : ICubeCoordDTO
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public CubeCoord ToModel() => new CubeCoord(X, Y, Z);
}
