using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileView : MonoBehaviour
{
    [SerializeField] private BattlefieldConfig colorPalette;
    private SpriteRenderer spriteRenderer;
    private TileController controller;

    /* -------- Init llamado por TileController ---------- */
    public void Init(TileController ctrl)
    {
        controller = ctrl;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Suscribimos el cambio de highlight
        controller.OnHighlightCurrentChanged += SetColor;

        // Color base
        SetColor(ETileHighlightType.None);
    }

    private void OnDestroy()
    {
        if (controller != null)
            controller.OnHighlightCurrentChanged -= SetColor;
    }

    public void SetColor(ETileHighlightType key)
    {
        spriteRenderer.color = colorPalette.GetColor(key);
    }
}
