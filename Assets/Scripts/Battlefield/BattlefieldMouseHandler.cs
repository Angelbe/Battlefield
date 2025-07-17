using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldMouseHandler : IBattlefieldMouseHandler
{
    private readonly Dictionary<CubeCoord, TileController> tileControllers;
    private readonly BattlefieldConfig config;
    private readonly PhaseManager PhaseManager;

    private TileController currentTileHovered;
    private TileController currentTileClicked;

    public event Action<TileController> OnTileHovered;
    public event Action OnTileUnhovered;
    public event Action<TileController> OnTileClickedDeploymentPhase;
    public event Action<TileController> OnTileClickedCombatPhase;

    private Color hoverColor => config.GetColor(ETileHighlightType.Hover);
    private Color selectedColor => config.GetColor(ETileHighlightType.Selected);

    public BattlefieldMouseHandler(Dictionary<CubeCoord, TileController> newTileControllers, BattlefieldConfig battlefieldConfig, PhaseManager newPhaseManager)
    {
        tileControllers = newTileControllers;
        config = battlefieldConfig;
        PhaseManager = newPhaseManager;
    }

    public void HandleHoverTile(TileController tileHovered)
    {
        var tile = tileControllers[tileHovered.Model.Coord];
        currentTileHovered = tile;
        currentTileHovered.Highlight.AddColor(4, hoverColor);
        OnTileHovered?.Invoke(currentTileHovered);
    }

    public void HandleUnhoverTile()
    {
        if (currentTileHovered == null || currentTileHovered == currentTileClicked)
            return;

        currentTileHovered.Highlight.RemoveColor(4, hoverColor);
        currentTileHovered = null;
        OnTileUnhovered?.Invoke();
    }

    private void handleClickOnDeploymentPhase(TileController tileClicked)
    {
        OnTileClickedDeploymentPhase?.Invoke(tileClicked);

    }
    private void handleClickOnCombatPhase(TileController tileClicked)
    {
        OnTileClickedCombatPhase?.Invoke(tileClicked);

    }

    public void HandleClickTile(TileController tileClicked)
    {
        CubeCoord tileCoords = tileClicked.Model.Coord;
        if (tileControllers[tileCoords] == null) return;

        if (PhaseManager.CurrentPhase == PhaseManager.DeploymentPhase) handleClickOnDeploymentPhase(tileClicked);
        if (PhaseManager.CurrentPhase == PhaseManager.CombatPhase) handleClickOnCombatPhase(tileClicked);

    }
}
