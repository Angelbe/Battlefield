using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public Guid ID { get; private set; }
    public BattlefieldController bfController { get; private set; }
    public CreatureModel Model { get; private set; }
    public CreatureStats Stats { get; private set; }
    public CreatureMovementHandler Movement { get; private set; }
    public CreatureCombatHandler Combat { get; private set; }
    public CreatureValidatorHandler Validator { get; private set; }
    private CreatureShapeCatalog shapeCatalog = new();
    public bool IsDefender { get; private set; }
    public List<TileController> OccupiedTiles { get; private set; }
    public bool isDead { get; private set; }
    public int Quantity { get; private set; }
    public Army Army { get; private set; }
    public CreatureView View;
    public TextMeshPro QuantityText;

    public void SetQuantityFromHealth()
    {
        int hpPerUnit = Model.HealthPoint;
        Quantity = Mathf.CeilToInt((float)Stats.HealthPoint / hpPerUnit);
        if (Quantity <= 0)
        {
            isDead = true;
            Death();

        }
    }

    public void Death()
    {
        ClearOccupiedTiles();
        bfController.PhaseManager.CombatPhase.HandleCreatureDeath(this);
        Destroy(gameObject);
    }


    public void SetAsDefender(bool isDefender)
    {
        IsDefender = isDefender;
        if (isDefender)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void ClearOccupiedTiles()
    {
        foreach (TileController oldTile in OccupiedTiles)
        {
            if (oldTile.OccupantCreature == this)
                oldTile.ClearOcupantCreature(this);
        }
    }

    private void SetOccupiedTiles(TileController AnchorTile)
    {
        CubeCoord center = AnchorTile.Model.Coord;
        List<CubeCoord> shapeOffsets = shapeCatalog.GetShape(Model.Shape);
        List<TileController> newTiles = new();

        foreach (var offset in shapeOffsets)
        {
            CubeCoord tileCoord = center + offset;
            if (bfController.BfGrid.TilesInTheBattlefield.TryGetValue(tileCoord, out var tile))
            {
                tile.SetOcupantCreature(this);
                newTiles.Add(tile);
            }
        }

        OccupiedTiles = newTiles;
    }

    public void SetNewPosition(TileController newTile)
    {
        if (OccupiedTiles != null) ClearOccupiedTiles();
        SetOccupiedTiles(newTile);
    }

    public List<TileController> GetAdjacentTiles()
    {
        List<TileController> result = new();

        foreach (TileController tile in OccupiedTiles)
            AddAdjacentTiles(tile, result);

        return result;
    }

    private void AddAdjacentTiles(TileController tile, List<TileController> result)
    {
        foreach (CubeCoord dir in CubeCoord.CubeDirections.Values)
        {
            CubeCoord neighborCoord = tile.Model.Coord + dir;
            if (!bfController.BfGrid.TilesInTheBattlefield.TryGetValue(neighborCoord, out TileController neighbor)) continue;
            if (IsOccupiedByThisCreature(neighbor)) continue;
            if (result.Contains(neighbor)) continue;

            result.Add(neighbor);
        }
    }

    private bool IsOccupiedByThisCreature(TileController tile)
    {
        foreach (TileController t in OccupiedTiles)
            if (t == tile) return true;

        return false;
    }

    public void CalculateHurt(int damage)
    {
        Stats.ModifyStat(ECreatureStat.Health, damage);

        if (Stats.HealthPoint <= 0)
        {
            Stats.HealthPoint = 0;
            Quantity = 0;
            isDead = true;
            Death();
            return;
        }

        SetQuantityFromHealth();
    }



    public void Init(CreatureModel model,
    BattlefieldController newBfController,
    Army newArmy,
    Guid CreatureStackID,
    TileController tileClicked,
    int newQuantity
    )
    {
        Model = model;
        Quantity = newQuantity;
        bfController = newBfController;
        Army = newArmy;
        ID = CreatureStackID;
        View.Init(Model);
        Stats = new CreatureStats(Model);
        Movement = new CreatureMovementHandler(this, bfController, new Pathfinder(newBfController));
        Combat = new CreatureCombatHandler(this, bfController);
        Validator = new CreatureValidatorHandler(this, bfController, shapeCatalog);
        SetNewPosition(tileClicked);
        if (newArmy.IsDefender != IsDefender)
        {
            SetAsDefender(newArmy.IsDefender);
        }
    }
}
