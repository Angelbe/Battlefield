using System;

public class TileHighlightController
{
    public ETileHighlightType originalHl { get; private set; } = ETileHighlightType.Transparent; // Cambiar esto a un  sistema de niveles (nivel 1 transparente nivel 2 deployment, nivel 3 selected etc)
    public ETileHighlightType currentHl { get; private set; } = ETileHighlightType.Transparent;
    public event Action<ETileHighlightType> OnHighlightCurrentChanged;
    public event Action<ETileHighlightType> OnHighlightBaseChanged;
    public TileView View { get; private set; }

    public TileHighlightController(TileView tileView, ETileHighlightType originalHiglight = ETileHighlightType.Transparent)
    {
        View = tileView;
        originalHl = originalHiglight;
    }

    public void SetCurrentHighlight(ETileHighlightType newtemporaryHl)
    {
        if (currentHl == newtemporaryHl) return;
        currentHl = newtemporaryHl;
        OnHighlightCurrentChanged?.Invoke(newtemporaryHl);
        ApplyHighlight(newtemporaryHl);
    }

    public void SetOriginalHighLight(ETileHighlightType newOriginalHl)
    {
        originalHl = newOriginalHl;
        OnHighlightBaseChanged?.Invoke(originalHl);
        ApplyHighlight(originalHl);
    }

    public void ResetCurrentHighlight()
    {
        SetCurrentHighlight(originalHl);
    }
    public void ResetOriginalTypeSelectedToTransparent(ETileHighlightType typeTilesToreset)
    {
        if (typeTilesToreset == originalHl)
        {
            SetOriginalHighLight(ETileHighlightType.Transparent);
        }
    }

    private void ApplyHighlight(ETileHighlightType key)
    {
        View.SetColor(key);
    }


}