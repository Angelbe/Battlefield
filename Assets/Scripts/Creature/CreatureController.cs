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
    private CubeCoord[] positions;
    public bool IsDefender;
    public int Quantity;


    public void Init(CreatureModel model, CubeCoord anchor, int newQuantity, bool isDefender)
    {
        Model = model;
        Quantity = newQuantity;
        View.GetComponent<CreatureView>();
        View.Init(Model);
        if (isDefender)
        {
            SetAsDefender();
        }
    }

    public void addQuantity(int newCreaturesToAdd)
    {
        Quantity += newCreaturesToAdd;
    }

    public void removeQuantity(int newCreaturesToAdd)
    {
        Quantity += newCreaturesToAdd;
    }

    public void SetAsDefender()
    {
        IsDefender = true;
        View.FlipSprite();
    }
}
