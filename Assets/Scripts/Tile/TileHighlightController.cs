using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileHighlightController
{
    private readonly Dictionary<int, HashSet<Color>> highlightLevels = new();
    private readonly TileView view;
    private readonly MonoBehaviour coroutineHost;
    private Coroutine colorCycleCoroutine;
    private Color currentDisplayedColor = Color.clear;

    public TileHighlightController(TileView tileView)
    {
        view = tileView;
        coroutineHost = tileView; // Asumimos que TileView es MonoBehaviour
        for (int i = 1; i <= 5; i++)
            highlightLevels[i] = new HashSet<Color>();
    }

    public void AddColor(int level, Color color)
    {
        if (level < 1 || level > 5) return;
        if (highlightLevels[level].Add(color))
            ApplyHighestPriorityColor();
    }

    public void RemoveColor(int level, Color color)
    {
        if (level < 1 || level > 5) return;
        if (highlightLevels[level].Remove(color))
            ApplyHighestPriorityColor();
    }

    public void ClearLevel(int level)
    {
        if (level < 1 || level > 5) return;
        highlightLevels[level].Clear();
        ApplyHighestPriorityColor();
    }

    public void ClearAll()
    {
        foreach (var level in highlightLevels.Keys.ToList())
            highlightLevels[level].Clear();
        ApplyHighestPriorityColor();
    }

    private void ApplyHighestPriorityColor()
    {
        for (int level = 5; level >= 1; level--)
        {
            var colors = highlightLevels[level];
            if (colors.Count == 0) continue;

            StopCyclingColors();
            List<Color> colorList = colors.ToList();

            if (colorList.Count == 1)
            {
                var color = colorList[0];
                if (currentDisplayedColor != color)
                {
                    view.SetColor(color);
                    currentDisplayedColor = color;
                }
            }
            else
            {
                StartCyclingColors(colorList);
                currentDisplayedColor = colorList.Aggregate(Color.clear, (acc, c) => acc + c) / colorList.Count;
            }
            return;
        }

        // Si ningún nivel tiene color → transparente
        StopCyclingColors();
        var fallback = view.GetColorFromType(ETileHighlightType.Transparent);
        view.SetColor(fallback);
        currentDisplayedColor = fallback;

    }

    private void StartCyclingColors(List<Color> colors)
    {
        StopCyclingColors();
        colorCycleCoroutine = coroutineHost.StartCoroutine(CycleColors(colors));
    }

    private void StopCyclingColors()
    {
        if (colorCycleCoroutine != null)
        {
            coroutineHost.StopCoroutine(colorCycleCoroutine);
            colorCycleCoroutine = null;
        }
    }

    private IEnumerator CycleColors(List<Color> colors)
    {
        int index = 0;
        float transitionDuration = 1f;

        while (true)
        {
            Color startColor = view.GetCurrentColor();
            Color targetColor = colors[index];
            float elapsed = 0f;

            while (elapsed < transitionDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / transitionDuration;
                view.SetRawColor(Color.Lerp(startColor, targetColor, t));
                yield return null;
            }

            view.SetRawColor(targetColor);
            index = (index + 1) % colors.Count;
            yield return null;
        }
    }
    public bool HasColorInLevel(int level, Color color)
    {
        return highlightLevels.TryGetValue(level, out var colors) && colors.Contains(color);
    }

}
