using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class CombatPhase : IBattlePhase
{
    private BattlefieldController bfController;
    private UICombatController uICombatController;
    public TurnHandler TurnHandler;
    public DeployHandler AttackerCreatures;
    public DeployHandler DefenderCreatures;
    public CreatureController ActiveCreature;
    public Army ActiveArmy;

    public CombatPhase(BattlefieldController newBFController, UIController newUIController)
    {
        bfController = newBFController;
        uICombatController = newUIController.UICombatController;
    }


    public void UpdateUITurnOrder()
    {
        var upcoming = TurnHandler.GetNextTurnOrder(15);
        uICombatController.ShowTurnOrder(upcoming);
    }

    public void SetNewActiveCreature(CreatureController newActiveCreature)
    {
        ActiveCreature = newActiveCreature;
        ActiveCreature.Movement.SetReachableTiles();
    }

    public void HandleCreatureFinishedTurn()
    {
        UpdateUITurnOrder();
        CreatureController nextCreature = TurnHandler.GetNextCreature();
        SetNewActiveCreature(nextCreature);
    }

    private void HandleClickTile(TileController tileClicked)
    {
        if (TryHandleRangedAttack(tileClicked)) return;
        if (TryHandleMeleeAttack(tileClicked)) return;
        if (TryHandleMovement(tileClicked)) return;
    }

    private bool TryHandleMeleeAttack(TileController tileClicked)
    {
        CreatureController occupant = tileClicked.OccupantCreature;

        if (occupant == null || occupant.Army == ActiveCreature.Army) return false;
        if (!ActiveCreature.Combat.CanMeleeAttack(occupant)) return false;

        TileController attackFromTile = ActiveCreature.Combat.FindClosestAttackTile(occupant, tileClicked);
        if (attackFromTile == null) return false;

        CubeCoord destination = attackFromTile.Model.Coord;
        bool isCurrentTile = ActiveCreature.OccupiedTiles.Exists(t => t.Model.Coord == destination);

        if (!isCurrentTile && !ActiveCreature.Movement.IsTileReachable(destination)) return false;

        if (isCurrentTile)
        {
            ActiveCreature.Combat.ExecuteMeleeAttack(occupant);
        }
        else
        {
            List<CubeCoord> path = ActiveCreature.Movement.Pathfinder.GetPath(
                ActiveCreature.OccupiedTiles[0].Model.Coord,
                destination
            );

            ActiveCreature.Movement.ClearReachableTiles();
            foreach (TileController tile in ActiveCreature.OccupiedTiles)
                tile.ClearOcupantCreature(ActiveCreature);

            ActiveCreature.Movement.MoveAlongPath(path, () =>
            {
                ActiveCreature.Combat.ExecuteMeleeAttack(occupant);
            });
        }

        return true;
    }


    private bool TryHandleRangedAttack(TileController tileClicked)
    {
        CreatureController occupant = tileClicked.OccupantCreature;

        if (occupant == null || occupant.Army == ActiveCreature.Army) return false;
        if (ActiveCreature.Model.AttackType != EAttackType.Range) return false;
        if (!ActiveCreature.Combat.CanRangedAttack(occupant)) return false;

        ActiveCreature.Combat.ExecuteRangedAttack(occupant);
        return true;
    }

    private bool TryHandleMovement(TileController tileClicked)
    {
        CubeCoord destination = tileClicked.Model.Coord;
        if (!ActiveCreature.Movement.IsTileReachable(destination)) return false;

        List<CubeCoord> path = ActiveCreature.Movement.Pathfinder.GetPath(
            ActiveCreature.OccupiedTiles[0].Model.Coord,
            destination
        );

        ActiveCreature.Movement.ClearReachableTiles();
        foreach (TileController tile in ActiveCreature.OccupiedTiles)
            tile.ClearOcupantCreature(ActiveCreature);

        ActiveCreature.Movement.MoveAlongPath(path, () =>
        {
            HandleCreatureFinishedTurn();
        });

        return true;
    }

    public void HandleCreatureDeath(CreatureController creature)
    {
        TurnHandler.RemoveCreature(creature);

        DeployHandler handler = creature.IsDefender ? DefenderCreatures : AttackerCreatures;
        handler.RemoveStackFromDeploy(creature.ID);
        CheckCreaturesRemaining();
    }

    public void CheckCreaturesRemaining()
    {
        bool attackerAlive = AttackerCreatures.Deployed.Count > 0;
        bool defenderAlive = DefenderCreatures.Deployed.Count > 0;

        if (attackerAlive && defenderAlive) return;

        Army winner = attackerAlive ? bfController.bfModel.Attacker : bfController.bfModel.Defender;
        EndCombat(winner);
    }


    private void EndCombat(Army armyWinner)
    {
        Debug.Log($"⚔ El ejército de {armyWinner.Champion.Name} es el vencedor.");
    }



    public void StartPhase()
    {
        AttackerCreatures = bfController.bfModel.Attacker.Deployed;
        DefenderCreatures = bfController.bfModel.Defender.Deployed;
        TurnHandler = new(AttackerCreatures, DefenderCreatures);
        uICombatController.Init();
        HandleCreatureFinishedTurn();
        bfController.BfMouse.OnTileClickedCombatPhase += HandleClickTile;

    }

    public void ExitPhase()
    {
        uICombatController.Shutdown();
        bfController.BfMouse.OnTileClickedCombatPhase -= HandleClickTile;
    }
}