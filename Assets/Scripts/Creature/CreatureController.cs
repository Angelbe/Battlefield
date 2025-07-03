using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public interface ICreatureController
{
    public CreatureView View { get; }
    public CreatureModel Model { get; }
    public void addQuantity(int newCreaturesToAdd);
    public void removeQuantity(int newCreaturesToAdd);
}

public class CreatureController : MonoBehaviour
{
    public Guid InstanceId { get; private set; } = Guid.NewGuid();
    public CreatureView View { get; private set; }
    public CreatureModel Model { get; private set; }
    private CubeCoord[] positions;
    public int Quantity;


    public void Init(CreatureModel model, CubeCoord anchor, int newQuantity)
    {
        Model = model;
        Quantity = newQuantity;
        View.GetComponent<CreatureView>();
        View.Init(Model);
    }

    public void addQuantity(int newCreaturesToAdd)
    {
        Quantity += newCreaturesToAdd;
    }

    public void removeQuantity(int newCreaturesToAdd)
    {
        Quantity += newCreaturesToAdd;
    }
}
