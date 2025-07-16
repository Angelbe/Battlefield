using System.Collections.Generic;
using UnityEngine;

public interface IBattlefieldController
{
    public Vector2 Center { get; }
    public BattlefieldConfig BfConfig { get; }
    public BattlefieldHighlightHandler BfHighlight { get; }
    public BattlefieldMouseHandler BfMouse { get; }
    public BattlefieldGridHandler BfGrid { get; }
    public BattlefieldDeploymentZones BfDeploymentZones { get; }
    public Dictionary<CubeCoord, TileController> TileControllers { get; }
    public void Init(BattlefieldModel newBfModel, BattlefieldConfig newBfConfig, CreatureCatalog creatureCatalog, UIController newIUController);
    public void PaintManyTiles(IEnumerable<CubeCoord> coord, ETileHighlightType newHighlightType);
    public void GenerateGrid();
}