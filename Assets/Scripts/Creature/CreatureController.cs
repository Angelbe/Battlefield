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
    private CreatureShapeCatalog shapeCatalog = new();
    public bool IsDefender { get; private set; }
    public CubeCoord[] Positions { get; private set; }
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
        if (IsDefender)
        {
            View.FlipSprite();
        }
    }

    public void SetNewPosition(TileController newTile)
    {
        CubeCoord center = newTile.Model.Coord;
        CubeCoord[] shapeOffsets = shapeCatalog.GetShape(Model.Shape);
        List<CubeCoord> newPositions = new();

        foreach (var offset in shapeOffsets)
        {
            CubeCoord tileCoord = center + offset;
            newPositions.Add(tileCoord);

            if (bfController.TileControllers.TryGetValue(tileCoord, out var tile))
            {
                tile.SetOcupantCreature(this);
            }
        }

        //La limpieza de ocupaciones deberñia ocurrir al empezar el movimiento y no al acabarlo
        if (Positions != null)
        {
            foreach (var oldCoord in Positions)
            {
                if (bfController.TileControllers.TryGetValue(oldCoord, out var tile))
                {
                    if (tile.OccupantCreature == this)
                        tile.ClearOcupantCreature(this);
                }
            }
        }

        Positions = newPositions.ToArray();
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
        SetNewPosition(tileClicked);
        if (isDefender)
        {
            SetAsDefender(isDefender);
        }
    }
}
