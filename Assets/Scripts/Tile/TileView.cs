using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void SetColor(Color targetColor)
    {
        StopAllCoroutines(); // por si estaba haciendo una transición anterior
        StartCoroutine(SmoothTransition(targetColor));
    }

    public void SetColors(List<Color> targetColors)
    {
        StopAllCoroutines(); // por si había otra transición
        StartCoroutine(SmoothTransitionMultiple(targetColors));
    }

    private IEnumerator SmoothTransitionMultiple(List<Color> colors)
    {
        float duration = 0.3f;
        float time = 0f;

        Color initialColor = spriteRenderer.color;
        Color averageColor = colors.Aggregate(Color.clear, (acc, c) => acc + c) / colors.Count;

        while (time < duration)
        {
            time += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(initialColor, averageColor, time / duration);
            yield return null;
        }

        spriteRenderer.color = averageColor;
    }

    private IEnumerator SmoothTransition(Color targetColor)
    {
        float duration = 0.1f;
        float time = 0f;
        Color initialColor = spriteRenderer.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, time / duration);
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }

    public void SetRawColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public Color GetCurrentColor()
    {
        return spriteRenderer.color;
    }


    public Color GetColorFromType(ETileHighlightType type)
    {
        return bfConfig.GetColor(type);
    }
}
