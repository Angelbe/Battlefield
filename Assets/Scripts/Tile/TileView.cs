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
        SetColor(bfConfig.GetColor(ETileHighlightType.Transparent));
    }

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    public Color GetColorFromType(ETileHighlightType type)
    {
        return bfConfig.GetColor(type);
    }
}
