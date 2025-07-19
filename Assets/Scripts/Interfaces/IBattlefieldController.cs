using System.Collections.Generic;
using UnityEngine;

public interface IBattlefieldController
{
    public BattlefieldConfig BfConfig { get; }
    public BattlefieldHighlightHandler BfHighlight { get; }
    public BattlefieldMouseHandler BfMouse { get; }
    public BattlefieldGridHandler BfGrid { get; }
    public BattlefieldDeploymentZones BfDeploymentZones { get; }
    public void Init(BattlefieldModel newBfModel,
    BattlefieldConfig newBfConfig,
    CursorBattlefieldController newCursor,
    PhaseManager newPhaseManager,
    CreatureCatalog creatureCatalog,
    UIController newIUController
    );
}