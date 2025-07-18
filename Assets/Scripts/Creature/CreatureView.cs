// CreatureView.cs
using UnityEngine;

public class CreatureView : MonoBehaviour, ICreatureView
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public CreatureModel Model { get; private set; }

    public void Init(CreatureModel model)
    {
        Model = model;
    }
    public void SetFlipSprite(bool isflip)
    {
        spriteRenderer.flipX = isflip;
    }
}
