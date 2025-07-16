using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public Guid InstanceId { get; private set; } = Guid.NewGuid();
    public CreatureView View { get; private set; }
    public CreatureModel Model { get; private set; }
    public CreatureStats Stats { get; private set; }
    private CubeCoord[] positions;
    public bool IsDefender;
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

    public void SetAsDefender()
    {
        IsDefender = true;
        View.FlipSprite();
    }

    public void Init(CreatureModel model, CubeCoord anchor, int newQuantity, bool isDefender)
    {
        Model = model;
        Quantity = newQuantity;
        View.GetComponent<CreatureView>();
        View.Init(Model);
        Stats = new CreatureStats(Model);
        if (isDefender)
        {
            SetAsDefender();
        }
    }
}
