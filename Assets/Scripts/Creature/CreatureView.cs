// CreatureView.cs
using UnityEngine;

public interface ICreatureView
{
    public void Init(CreatureModel model);
}

public class CreatureView : MonoBehaviour, ICreatureView
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public CreatureModel Model { get; private set; }

    public void Init(CreatureModel model)
    {
        Model = model;
    }
    public void FlipSprite()
    {
        spriteRenderer.flipX = true;
    }
}
