using System.Collections.Generic;
using UnityEngine;

public class CreatureCombatHandler
{
    private readonly CreatureController crController;
    private readonly BattlefieldController bfController;

    public CreatureCombatHandler(CreatureController controller, BattlefieldController battlefield)
    {
        crController = controller;
        bfController = battlefield;
    }

    private int CalculateDamage(CreatureController attacker, CreatureController defender)
    {
        int attack = attacker.Stats.Attack;
        int defense = defender.Stats.Defense;
        int min = attacker.Stats.MinDamage;
        int max = attacker.Stats.MaxDamage;
        int baseDmg = (min + max) / 2;

        float modifier = 1 + ((attack - defense) * 0.05f);
        modifier = Mathf.Max(0.2f, modifier);

        return Mathf.Max(1, Mathf.RoundToInt(baseDmg * attacker.Quantity * modifier));
    }


    public bool CanMeleeAttack(CreatureController target)
    {
        if (target == null || target.isDead) return false;
        if (target.Army == crController.Army) return false;

        HashSet<CubeCoord> reachableTiles = crController.Movement.ReachableTiles;
        reachableTiles.Add(crController.OccupiedTiles[0].Model.Coord);
        if (reachableTiles == null) return false;

        foreach (var enemyTile in target.OccupiedTiles)
        {
            foreach (var dir in CubeCoord.CubeDirections.Values)
            {
                CubeCoord adjacent = enemyTile.Model.Coord + dir;
                if (reachableTiles.Contains(adjacent))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool CanRangedAttack(CreatureController target)
    {
        if (target == null || target.isDead) return false;
        if (target.Army == crController.Army) return false;
        if (crController.Model.AttackType != EAttackType.Range) return false;

        // Comprobar si hay enemigos adyacentes bloqueando disparo
        foreach (TileController tile in crController.OccupiedTiles)
        {
            foreach (CubeCoord dir in CubeCoord.CubeDirections.Values)
            {
                CubeCoord adjacent = tile.Model.Coord + dir;
                if (!bfController.TileControllers.TryGetValue(adjacent, out TileController neighborTile)) continue;
                if (neighborTile.OccupantCreature != null && neighborTile.OccupantCreature.Army != crController.Army)
                {
                    return false; // bloqueo por melee
                }
            }
        }

        return true;
    }


    public TileController FindClosestAttackTile(CreatureController enemy, Vector3 cursorWorldPos)
    {
        TileController bestTile = null;
        float shortestDist = float.MaxValue;
        List<TileController> adjacentTiles = enemy.GetAdjacentTiles();

        foreach (TileController tile in adjacentTiles)
        {
            if (!crController.Movement.IsTileReachable(tile.Model.Coord)) continue;

            float dist = (tile.Model.WorldPosition - cursorWorldPos).sqrMagnitude;
            if (dist >= shortestDist) continue;

            bestTile = tile;
            shortestDist = dist;
        }

        return bestTile;
    }

    public void ExecuteMeleeAttack(CreatureController target)
    {

        int totalDamage = CalculateDamage(crController, target);
        target.ModifyQuantity(-totalDamage);
        Debug.Log($"🗡 {crController.name} hizo {totalDamage} de daño a {target.name} (quedan {target.Quantity})");

        if (!target.isDead && target.Stats.Retaliations > 0)
        {
            ExecuteRetaliation(target);
        }

        bfController.PhaseManager.CombatPhase.HandleCreatureFinishedTurn();
    }

    private void ExecuteRetaliation(CreatureController target)
    {

        int retaliationDamage = CalculateDamage(target, crController);
        crController.ModifyQuantity(-retaliationDamage);
        Debug.Log($"🔁 {target.name} contraatacó e hizo {retaliationDamage} de daño a {crController.name} (quedan {crController.Quantity})");
    }

    public void ExecuteRangedAttack(CreatureController target)
    {
        int totalDamage = CalculateDamage(crController, target);
        target.ModifyQuantity(-totalDamage);
        Debug.Log($"🏹 {crController.name} hizo {totalDamage} de daño a distancia a {target.name}");
        bfController.PhaseManager.CombatPhase.HandleCreatureFinishedTurn();
    }



}
