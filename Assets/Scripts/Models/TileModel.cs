public class TileModel
{
    public CubeCoord Cube { get; }
    public TileHighlightType Highlight { get; private set; }
    public UnitModel Occupant { get; private set; }
    public bool IsOccupied => Occupant != null;
    public event System.Action<TileHighlightType> OnHighlightChanged;


    public TileModel(CubeCoord cube)
    {
        Cube = cube;
        Highlight = TileHighlightType.None;
    }

    public void SetOccupant(UnitModel unit)
    {
        Occupant = unit;
    }

    public void SetHighlight(TileHighlightType value)
    {
        if (Highlight == value) return;      // evita fuego cruzado infinito
        Highlight = value;
        OnHighlightChanged?.Invoke(value);   // avisa a los listeners
    }
}