using UnityEngine;
using UnityEngine.UI;

public class UITurnOrderSlotView : MonoBehaviour
{
    public Image BackgroundImage;
    public Image CreatureImage;

    public void SetCreature(CreatureController creature)
    {
        var sprite = creature.GetComponentInChildren<SpriteRenderer>().sprite;
        CreatureImage.sprite = sprite;

        // Opcional: cambiar color del fondo según el ejército
        var color = creature.IsDefender ? Color.blue : Color.red;
        BackgroundImage.color = color;
    }
}
