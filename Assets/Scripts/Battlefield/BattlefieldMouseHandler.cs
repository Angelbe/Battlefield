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
    private CursorBattlefieldController uroborosCursor;

    public BattlefieldMouseHandler(Dictionary<CubeCoord, TileController> newTileControllers, BattlefieldConfig battlefieldConfig, PhaseManager newPhaseManager, CursorBattlefieldController newCursor)
    {
        tileControllers = newTileControllers;
        config = battlefieldConfig;
        PhaseManager = newPhaseManager;
        uroborosCursor = newCursor;
    }

    public void HandleHoverTile(TileController tileHovered)
    {
        if (PhaseManager.CurrentPhase == PhaseManager.DeploymentPhase) HandleHoverDuringDeploy(tileHovered);
        else if (PhaseManager.CurrentPhase == PhaseManager.CombatPhase) HandleHoverDuringCombat(tileHovered);

    }

    private void HandleHoverDuringDeploy(TileController tileHovered)
    {
        TileController tile = tileControllers[tileHovered.Model.Coord];
        currentTileHovered = tile;
        currentTileHovered.Highlight.AddColor(4, hoverColor);
        OnTileHovered?.Invoke(currentTileHovered);
    }

    private void HandleHoverDuringCombat(TileController tileHovered)
    {
        TileController tile = tileControllers[tileHovered.Model.Coord];
        currentTileHovered = tile;

        CreatureController active = PhaseManager.CombatPhase.ActiveCreature;
        CreatureController target = tile.OccupantCreature;

        tile.Highlight.AddColor(4, hoverColor);
        UpdateCursorIcon(active, target, tile);
    }

    private void UpdateCursorIcon(CreatureController active, CreatureController target, TileController tile)
    {
        if (ShouldShowRangedCursor(active, target))
        {
            uroborosCursor.SetCursor(ECursorType.RangedAttack);
            return;
        }
        if (ShouldShowMeleeCursor(active, target, tile))
        {
            uroborosCursor.SetCursor(ECursorType.MeleeAttack);
            return;
        }



        uroborosCursor.SetCursor(ECursorType.Default);
    }

    private bool ShouldShowMeleeCursor(CreatureController active, CreatureController target, TileController tile)
    {
        if (target == null || target.Army == active.Army) return false;

        foreach (CubeCoord dir in CubeCoord.CubeDirections.Values)
        {
            CubeCoord neighbor = tile.Model.Coord + dir;
            if (active.Movement.IsTileReachable(neighbor)) return true;
        }

        return false;
    }

    private bool ShouldShowRangedCursor(CreatureController active, CreatureController target)
    {
        if (target == null || target.Army == active.Army) return false;
        if (active.Model.AttackType != EAttackType.Range) return false;
        if (!active.Combat.CanRangedAttack(target)) return false;

        foreach (TileController adjacent in active.GetAdjacentTiles())
        {
            CreatureController occupant = adjacent.OccupantCreature;
            if (occupant != null && occupant.Army != active.Army)
                return false; // Hay enemigos adyacentes
        }

        return true;
    }

    public void HandleUnhoverTile()
    {
        if (currentTileHovered != null && currentTileHovered != currentTileClicked)
        {
            currentTileHovered.Highlight.RemoveColor(4, hoverColor);
            currentTileHovered = null;
        }
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
