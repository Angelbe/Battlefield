using System;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public Guid InstanceId { get; private set; } = Guid.NewGuid();
    public BattlefieldController bfController;
    public CreatureView View;
    public CreatureModel Model { get; private set; }
    public CreatureStats Stats { get; private set; }
    public MovementHandler Movement { get; private set; }
    public CreatureCombatHandler Combat { get; private set; }
    private CreatureShapeCatalog shapeCatalog = new();
    public bool IsDefender { get; private set; }
    public List<TileController> OccupiedTiles { get; private set; }
    public bool isDead;
    public int Quantity;
    public Army Army;

    public void ModifyQuantity(int newCreaturesToAdd)
    {
        Quantity += newCreaturesToAdd;
        if (Quantity <= 0)
        {
            isDead = true;
        }
    }

    public void SetAsDefender(bool isDefender)
    {
        IsDefender = isDefender;
        View.SetFlipSprite(isDefender);
    }

    public void SetNewPosition(TileController newTile)
    {

        if (OccupiedTiles != null)
        {
            foreach (var oldTile in OccupiedTiles)
            {
                if (oldTile.OccupantCreature == this)
                    oldTile.ClearOcupantCreature(this);
            }
        }


        CubeCoord center = newTile.Model.Coord;
        CubeCoord[] shapeOffsets = shapeCatalog.GetShape(Model.Shape);
        List<TileController> newTiles = new();

        foreach (var offset in shapeOffsets)
        {
            CubeCoord tileCoord = center + offset;
            if (bfController.TileControllers.TryGetValue(tileCoord, out var tile))
            {
                tile.SetOcupantCreature(this);
                newTiles.Add(tile);
            }
        }

        OccupiedTiles = newTiles;
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
            if (!bfController.TileControllers.TryGetValue(neighborCoord, out TileController neighbor)) continue;
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

    public void Init(CreatureModel model, BattlefieldController newBfController, Army newArmy, TileController tileClicked, int newQuantity, bool isDefender)
    {
        Model = model;
        Quantity = newQuantity;
        bfController = newBfController;
        Army = newArmy;
        View.Init(Model);
        Stats = new CreatureStats(Model);
        Movement = new MovementHandler(this, newBfController, new Pathfinder(newBfController.TileControllers));
        Combat = new CreatureCombatHandler(this, newBfController);
        SetNewPosition(tileClicked);
        if (isDefender)
        {
            SetAsDefender(isDefender);
        }
    }
}
