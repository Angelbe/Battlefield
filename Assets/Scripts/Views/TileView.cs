using System;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ColorPalette colorPalette;
    public TileModel Model { get; private set; }
    private Action<TileHighlightType> HighlightKeyChangeHandler;

    // El controller llama a esto justo después de instanciar la casilla
    public void Init(TileModel model)
    {
        Model = model;
        SetHighlightColor(TileHighlightType.None);          // color base
        HighlightKeyChangeHandler = colorKey => SetHighlightColor(colorKey);
        Model.OnHighlightChanged += HighlightKeyChangeHandler;
    }

    private void OnDestroy()
    {
        if (Model != null)
        {
            Model.OnHighlightChanged -= HighlightKeyChangeHandler;
        }
    }

    public void SetHighlightColor(TileHighlightType colorKey)
    {
        spriteRenderer.color = colorPalette.GetColor(colorKey);
    }

    /* ---------- Input ---------- */
    private void OnMouseDown() => BattlefieldController.Instance.OnTileClicked(Model.Cube);
    private void OnMouseEnter() => BattlefieldController.Instance.OnTileHovered(Model.Cube);
    private void OnMouseExit() => BattlefieldController.Instance.OnTileUnhovered(Model.Cube);
}
