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
    private readonly List<CubeCoord> positions = new();
    public int Quantity;


    public void Init(CreatureModel model, CubeCoord anchor, int newQuantity)
    {
        Model = model;
        Quantity = newQuantity;
        View.GetComponent<CreatureView>();
        View.Init(Model);
        RebuildPositions(anchor);
        UpdateWorldPosition();
    }

    public void MoveTo(CubeCoord newAnchor)
    {
        RebuildPositions(newAnchor);
        UpdateWorldPosition();
    }

    private void RebuildPositions(CubeCoord anchor)
    {
        positions.Clear();
        positions.AddRange(Model.Shape.Select(rel => anchor + rel));
    }

    private void UpdateWorldPosition()
    {
        transform.position = BattlefieldController.Instance.WorldPosOf(positions[0]) + Vector3.up * 0.01f;
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
