public class TileModel
{
    public CubeCoord Cube { get; }
    public ETileHighlightType Highlight { get; private set; }
    public CreatureModel Occupant { get; private set; }
    public bool IsOccupied => Occupant != null;
    public event System.Action<ETileHighlightType> OnHighlightChanged;


    public TileModel(CubeCoord cube)
    {
        Cube = cube;
        Highlight = ETileHighlightType.None;
    }

    public void SetOccupant(CreatureModel unit)
    {
        Occupant = unit;
    }

    public void SetHighlight(ETileHighlightType value)
    {
        if (Highlight == value) return;      // evita fuego cruzado infinito
        Highlight = value;
        OnHighlightChanged?.Invoke(value);   // avisa a los listeners
    }
}