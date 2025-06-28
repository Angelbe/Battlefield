using UnityEngine;

/* UnitController.cs (MonoBehaviour) */
public class UnitController : MonoBehaviour
{
    public UnitModel UnitModel { get; private set; }
    private UnitView unitView;

    /* Init desde Battlefield */
    public void Init(UnitModel initmodel, UnitView initView)
    {
        UnitModel = initmodel;
        this.unitView = initView;
        UnitModel.OnPositionChanged += pos => this.unitView.UpdateWorldPos(pos);
    }
    public void MoveUnit(UnitModel unit, CubeCoord newCenter, BattlefieldController board)
    {
        // 1 Libera las antiguas
        foreach (var tile in unit.OccupiedCoords)
            board.TileModels[tile].SetOccupant(null);

        // 2 Marca las nuevas
        unit.SetCenter(newCenter);
        foreach (var tile in unit.OccupiedCoords)
            board.TileModels[tile].SetOccupant(unit);

        // 3 Mueve la vista (solo al centro; las highlights salen del modelo)
        var uView = board.UnitViews[unit];
        uView.transform.position = board.WorldPosOf(newCenter) + Vector3.up * 0.01f;
    }

}
