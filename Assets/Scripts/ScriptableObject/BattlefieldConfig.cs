using UnityEngine;

[CreateAssetMenu(fileName = "BattlefieldConfig", menuName = "Uroboros/BattlefieldConfig")]
public class BattlefieldConfig : ScriptableObject
{
    public Color noneColor = Color.gray;
    public Color hoverColor = Color.cyan;
    public Color selectedColor = Color.yellow;
    public Color deployZoneColor = Color.green;
    public GameObject tilePrefab;
    public GameObject battlefieldPrefab;
    public int GridHeight = 11;
    public float HexSize = 0.65f;

    public Color GetColor(ETileHighlightType highlight)
    {
        return highlight switch
        {
            ETileHighlightType.Hover => hoverColor,
            ETileHighlightType.Selected => selectedColor,
            ETileHighlightType.DeployZone => deployZoneColor,
            _ => noneColor,
        };
    }
}
