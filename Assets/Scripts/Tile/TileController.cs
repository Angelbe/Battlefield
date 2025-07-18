using UnityEngine;

public class TileController : MonoBehaviour, ITileController
{
    public TileView View { get; private set; }
    public TileModel Model { get; private set; }
    public BattlefieldController BfController { get; private set; }
    public TileHighlightController Highlight { get; private set; }
    public CreatureController OccupantCreature { get; private set; }
    public AttackCursor AttackCursor { get; private set; }
    [SerializeField] private GameObject AttackCursorPrefab;

    public void Init(TileModel model, BattlefieldController newBfController)
    {
        Model = model;
        View = GetComponent<TileView>();
        View.Init();
        Highlight = new(View);
        BfController = newBfController;
    }

    public void SetOcupantCreature(CreatureController newOcuppantCreature)
    {
        OccupantCreature = newOcuppantCreature;
        Highlight.AddColor(2, OccupantCreature.Army.ArmyColor);
    }

    public void ClearOcupantCreature(CreatureController creatureTryingToClear)
    {
        if (OccupantCreature == creatureTryingToClear)
        {
            Highlight.RemoveColor(2, OccupantCreature.Army.ArmyColor);
            OccupantCreature = null;
        }
    }

    private void OnMouseEnter() => BfController.HandleHoverTile(this);
    private void OnMouseExit() => BfController.HandleUnhoverTile();
    private void OnMouseDown() => BfController.HandleclickTile(this);

    public void InstatiateAttackCursor()
    {

    }

    public void Update()
    {

    }
}
