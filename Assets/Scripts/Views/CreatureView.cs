// CreatureView.cs (simplísimo)
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CreatureView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color selectedTint = Color.cyan;

    public CreatureModel Model { get; private set; }
    private Color original;

    public void Init(CreatureModel model)
    {
        Model = model;
        original = spriteRenderer.color;
    }

    public void UpdateWorldPos(CubeCoord cube)
    {
        transform.position = BattlefieldController.Instance.WorldPosOf(cube) + Vector3.up * 0.01f;
    }

    public void SetSelected(bool sel)
        => spriteRenderer.color = sel ? selectedTint : original;
}
