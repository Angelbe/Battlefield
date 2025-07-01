// CreatureView.cs
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CreatureView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public CreatureModel Model { get; private set; }

    public void Init(CreatureModel model)
    {
        Model = model;
        spriteRenderer.sprite = model.Sprite;
    }

    public void UpdateWorldPos(CubeCoord cube)
    {
        transform.position = BattlefieldController.Instance.WorldPosOf(cube) + Vector3.up * 0.01f;
    }
}
