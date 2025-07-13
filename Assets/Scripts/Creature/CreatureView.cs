// CreatureView.cs
using UnityEngine;

public interface ICreatureView
{
    public void Init(Creature model);
}

[RequireComponent(typeof(SpriteRenderer))]
public class CreatureView : MonoBehaviour, ICreatureView
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Creature Model { get; private set; }

    public void Init(Creature model)
    {
        Model = model;
    }
}
