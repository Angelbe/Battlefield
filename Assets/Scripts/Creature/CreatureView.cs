// CreatureView.cs
using UnityEngine;

public class CreatureView : MonoBehaviour, ICreatureView
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject QuantityContainer;
    public CreatureModel Model { get; private set; }
    private bool isDefender;

    public void Init(CreatureModel model)
    {
        Model = model;
    }

    public void SetIsDefender(bool newIsDefender)
    {
        isDefender = newIsDefender;
        spriteRenderer.flipX = isDefender;
        FlipQuantityContainer();
    }

    private void FlipQuantityContainer()
    {
        Vector3 pos = QuantityContainer.transform.localPosition;
        pos.x = Mathf.Abs(pos.x); // restaurar original si atacante
        pos.y += 0.3f;
        QuantityContainer.transform.localPosition = isDefender ? pos : QuantityContainer.transform.localPosition; ;
    }

}
