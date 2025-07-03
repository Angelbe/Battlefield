using System;
using UnityEngine;

/// <remarks>
/// Representa una posición en una cuadrícula “offset” (columna-fila).
/// Ideal como clave en diccionarios o para lógica de pathfinding.
/// </remarks>
public readonly struct ColRow : IEquatable<ColRow>
{
    public int Col { get; }
    public int Row { get; }

    public ColRow(int col, int row)
    {
        Col = col;
        Row = row;
    }

    /* ───── Deconstrucción (tupla) ───── */
    public void Deconstruct(out int col, out int row)
    {
        col = Col;
        row = Row;
    }

    /* ───── Igualdad & hashing ───── */
    public bool Equals(ColRow other) => Col == other.Col && Row == other.Row;
    public override bool Equals(object obj) => obj is ColRow other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(Col, Row);

    public static bool operator ==(ColRow left, ColRow right) => left.Equals(right);
    public static bool operator !=(ColRow left, ColRow right) => !(left == right);

    /* ───── Operadores aritméticos opcionales ───── */
    public static ColRow operator +(ColRow a, ColRow b) => new ColRow(a.Col + b.Col, a.Row + b.Row);
    public static ColRow operator -(ColRow a, ColRow b) => new ColRow(a.Col - b.Col, a.Row - b.Row);

    /* ───── Conversión explícita a Vector2Int (útil para Gizmos, etc.) ───── */
    public static explicit operator Vector2Int(ColRow cr) => new Vector2Int(cr.Col, cr.Row);
    public static ColRow FromCubeCoord(CubeCoord coord)
    {
        int row = coord.Z;
        int col = coord.X + (row - (row & 1)) / 2;
        return new ColRow(col, row);
    }
    public static Vector2 FromColRowToWorldPosition(ColRow colRow, float size)
    {
        float width = Mathf.Sqrt(3f) * size;
        float height = 1.5f * size;
        float offsetX = (colRow.Row % 2 == 0) ? 0 : width / 2f;
        return new Vector2(colRow.Col * width + offsetX, colRow.Row * height);
    }
    public override string ToString() => $"({Col}, {Row})";
}
