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
    private CursorBattlefieldController cursor;

    public BattlefieldMouseHandler(Dictionary<CubeCoord, TileController> newTileControllers, BattlefieldConfig battlefieldConfig, PhaseManager newPhaseManager, CursorBattlefieldController newCursor)
    {
        tileControllers = newTileControllers;
        config = battlefieldConfig;
        PhaseManager = newPhaseManager;
        cursor = newCursor;
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

        if (TryShowMeleeHover(active, target, tile)) return;
        if (TryShowRangedHover(active, target, tile)) return;

        tile.Highlight.AddColor(4, hoverColor);
    }

    private bool TryShowMeleeHover(CreatureController attacker, CreatureController target, TileController hoveredTile)
    {
        if (target == null || target.Army == attacker.Army) return false;
        if (!attacker.Combat.CanMeleeAttack(target)) return false;

        TileController attackFrom = attacker.Combat.FindClosestAttackTile(target, hoveredTile.Model.WorldPosition);
        if (attackFrom == null) return false;

        cursor.SetCursor(ECursorType.MeleeAttack);
        attackFrom.Highlight.AddColor(4, hoverColor);
        currentTileHovered = attackFrom;
        return true;
    }

    private bool TryShowRangedHover(CreatureController attacker, CreatureController target, TileController hoveredTile)
    {
        if (target == null || target.Army == attacker.Army) return false;
        if (attacker.Model.AttackType != EAttackType.Range) return false;
        if (!attacker.Combat.CanRangedAttack(target)) return false;
        cursor.SetCursor(ECursorType.RangedAttack);
        hoveredTile.Highlight.AddColor(4, hoverColor);
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
