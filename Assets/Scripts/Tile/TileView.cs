using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileView : MonoBehaviour, ITileView
{
    [SerializeField] private BattlefieldConfig bfConfig;
    private SpriteRenderer spriteRenderer;

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor(ETileHighlightType.Transparent);
    }

    public void SetColor(ETileHighlightType newColor)
    {
        spriteRenderer.color = bfConfig.GetColor(newColor);
    }
}
