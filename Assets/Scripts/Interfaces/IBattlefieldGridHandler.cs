using System.Collections.Generic;

public interface IBattlefieldGridHandler
{
    public Dictionary<CubeCoord, TileModel> GenerateGridModel(int numberOfRows, int numberOfColumns, float HexSize);
}