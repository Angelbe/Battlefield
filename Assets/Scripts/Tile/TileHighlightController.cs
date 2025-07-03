using System;

public class TileHighlightController
{
    public ETileHighlightType originalHl { get; private set; } = ETileHighlightType.Transparent;
    public ETileHighlightType currentHl { get; private set; } = ETileHighlightType.Transparent;
    public event Action<ETileHighlightType> OnHighlightCurrentChanged;
    public event Action<ETileHighlightType> OnHighlightBaseChanged;
    public TileView View { get; private set; }

    public TileHighlightController(TileView tileView, ETileHighlightType originalHiglight = ETileHighlightType.Transparent)
    {
        View = tileView;
        originalHl = originalHiglight;
    }

    public void SetHighlight(ETileHighlightType newtemporaryHl)
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

    public void ResetHighlight()
    {
        SetHighlight(originalHl);
    }

    private void ApplyHighlight(ETileHighlightType key)
    {
        View.SetColor(key);
    }


}