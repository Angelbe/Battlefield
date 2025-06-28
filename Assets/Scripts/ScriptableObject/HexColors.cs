using UnityEngine;

[CreateAssetMenu(fileName = "NewColorPalette", menuName = "Uroboros/Color Palette")]
public class ColorPalette : ScriptableObject
{
    public Color noneColor = Color.gray;
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;

    public Color GetColor(TileHighlightType highlight)
    {
        return highlight switch
        {
            TileHighlightType.Hover => hoverColor,
            TileHighlightType.Selected => selectedColor,
            _ => noneColor,
        };
    }
}
