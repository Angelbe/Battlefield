using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileView : MonoBehaviour, ITileView
{
    [SerializeField] private BattlefieldConfig colorPalette;
    private SpriteRenderer spriteRenderer;

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor(ETileHighlightType.Transparent);
    }

    public void SetColor(ETileHighlightType newColor)
    {
        spriteRenderer.color = colorPalette.GetColor(newColor);
    }
}
