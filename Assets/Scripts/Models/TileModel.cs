public class TileModel
{
    public CubeCoord CubeCoord { get; }
    public CreatureModel OccupantModel { get; private set; }
    public bool IsOccupied => OccupantModel != null;

    public TileModel(CubeCoord cubeCoord)
    {
        CubeCoord = cubeCoord;
    }

    public void SetOccupant(CreatureModel creature)
    {
        OccupantModel = creature;
    }

    public void ClearOccupant()
    {
        OccupantModel = null;
    }
}
