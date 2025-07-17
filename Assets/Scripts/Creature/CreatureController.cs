using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public Guid InstanceId { get; private set; } = Guid.NewGuid();
    public CreatureView View;
    public CreatureModel Model { get; private set; }
    public CreatureStats Stats { get; private set; }
    private CubeCoord[] positions;
    public bool IsDefender { get; private set; }
    public bool isDead;
    public int Quantity;

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

    public void Init(CreatureModel model, CubeCoord anchor, int newQuantity, bool isDefender)
    {
        Model = model;
        Quantity = newQuantity;
        View.Init(Model);
        Stats = new CreatureStats(Model);
        if (isDefender)
        {
            SetAsDefender(isDefender);
        }
    }
}
