using System;
using UnityEngine;

public interface ITileController
{
    BattlefieldController BfController { get; }
    TileView View { get; }
    TileModel Model { get; }
    TileHighlightController Highlight { get; }
    public void PaintTile(ETileHighlightType newHighLightType);
}

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
        Highlight = new(View);
        BfController = newBfController;
    }

    public void PaintTile(ETileHighlightType newHighlight)
    {
        Highlight.SetCurrentHighlight(newHighlight);
    }

    public void PaintOriginalHlTile(ETileHighlightType newHighlight)
    {
        Highlight.SetOriginalHighLight(newHighlight);
    }

    public void ResetPaint()
    {
        Highlight.ResetCurrentHighlight();
    }

    public void ResetOriginalTypeHiglightedToTrransparent(ETileHighlightType TypeToReset)
    {
        Highlight.ResetOriginalTypeSelectedToTransparent(TypeToReset);
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
