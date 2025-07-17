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

        // Cambia el color de fondo según el ejército
        BackgroundImage.color = creature.IsDefender ? Color.blue : Color.red;

        // Gira horizontalmente si es defensor (escala X negativa)
        Vector3 scale = CreatureImage.rectTransform.localScale;
        scale.x = creature.IsDefender ? -1f : 1f;
        CreatureImage.rectTransform.localScale = scale;
    }
}
