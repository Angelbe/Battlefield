using System;
using UnityEngine;

public readonly struct ColRow : IEquatable<ColRow>
{
    public int Col { get; }
    public int Row { get; }

    public ColRow(int col, int row)
    {
        Col = col;
        Row = row;
    }

    public void Deconstruct(out int col, out int row)
    {
        col = Col;
        row = Row;
    }

    public bool Equals(ColRow other) => Col == other.Col && Row == other.Row;
    public override bool Equals(object obj) => obj is ColRow other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Col, Row);

    public static bool operator ==(ColRow left, ColRow right) => left.Equals(right);
    public static bool operator !=(ColRow left, ColRow right) => !(left == right);

    public static ColRow operator +(ColRow a, ColRow b) => new ColRow(a.Col + b.Col, a.Row + b.Row);
    public static ColRow operator -(ColRow a, ColRow b) => new ColRow(a.Col - b.Col, a.Row - b.Row);

    public static explicit operator Vector2Int(ColRow cr) => new Vector2Int(cr.Col, cr.Row);
    public static ColRow FromCubeCoord(CubeCoord coord)
    {
        int row = coord.Z;
        int col = coord.X + (row - (row & 1)) / 2;
        return new ColRow(col, row);
    }
    public static Vector3 FromColRowToWorldPosition(ColRow colRow, float size)
    {
        float width = Mathf.Sqrt(3f) * size;
        float height = 1.5f * size;
        float offsetX = (colRow.Row % 2 == 0) ? 0 : width / 2f;
        return new Vector3(colRow.Col * width + offsetX, colRow.Row * height);
    }
    public override string ToString() => $"({Col}, {Row})";
}
