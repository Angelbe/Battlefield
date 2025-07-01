using System;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public TileView View { get; private set; }
    public TileModel Model { get; private set; }
    public ETileHighlightType BaseKey = ETileHighlightType.None;
    private ETileHighlightType current = ETileHighlightType.None;
    public event Action<ETileHighlightType> OnHighlightCurrentChanged;
    public event Action<ETileHighlightType> OnHighlightBaseChanged;

    public void Init(TileModel model)
    {
        Model = model;
        View = GetComponent<TileView>();
        View.Init(this); // Iniciamos también la vista
        ApplyHighlight(BaseKey); // Aplicamos el estado visual base
    }

    public void SetHighlight(ETileHighlightType key, bool asBase = false)
    {
        if (asBase)
        {
            BaseKey = key;
            OnHighlightBaseChanged?.Invoke(key);
        }
        if (current == key) return;

        current = key;
        OnHighlightCurrentChanged?.Invoke(key);
        ApplyHighlight(key);
    }

    public void ResetHighlight() => SetHighlight(BaseKey);

    private void ApplyHighlight(ETileHighlightType key)
    {
        View.SetColor(key);          // delega en la vista la paleta real
    }

    /* ---------- Input ---------- */
    private void OnMouseEnter() => BattlefieldController.Instance.OnTileHovered(Model.CubeCoord);
    private void OnMouseExit() => BattlefieldController.Instance.OnTileUnhovered(Model.CubeCoord);
    private void OnMouseDown() => BattlefieldController.Instance.OnTileClicked(Model.CubeCoord);
}
