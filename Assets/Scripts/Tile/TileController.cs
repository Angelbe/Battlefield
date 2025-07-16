using UnityEngine;

public class TileController : MonoBehaviour, ITileController
{
    public TileView View { get; private set; }
    public TileModel Model { get; private set; }
    public BattlefieldController BfController { get; private set; }
    public TileHighlightController Highlight { get; private set; }
    public CreatureController OccupantCreature { get; private set; }

    public void Init(TileModel model, BattlefieldController newBfController)
    {
        Model = model;
        View = GetComponent<TileView>();
        View.Init();
        Highlight = new(View); // Aquí View actúa como MonoBehaviour para StartCoroutine
        BfController = newBfController;
    }

    public void SetOcupantCreature(CreatureController newOcuppantCreature)
    {
        OccupantCreature = newOcuppantCreature;
    }

    public void ClearOcupantCreature()
    {
        OccupantCreature = null;
    }

    private void OnMouseEnter() => BfController.HandleHoverTile(Model.Coord);
    private void OnMouseExit() => BfController.HandleUnhoverTile();
    private void OnMouseDown() => BfController.HandleclickTile(Model.Coord);
}
