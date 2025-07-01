using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureController : MonoBehaviour
{

    public CreatureView View { get; private set; }
    public CreatureModel Model { get; private set; }
    private readonly List<CubeCoord> positions = new();

    public void Init(CreatureModel model, CubeCoord anchor)
    {
        Model = model;
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
}
