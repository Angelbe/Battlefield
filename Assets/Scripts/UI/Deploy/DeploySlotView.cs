using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeploySlotView : MonoBehaviour
{
    public bool isSelected { get; private set; } = false;
    public Image CreatureImage;
    public GameObject SelectionFrame;
    [SerializeField] private CreatureCatalog creatureCatalog;

    public void SelectSlot()
    {
        isSelected = true;
        SelectionFrame.SetActive(true);
    }

    public void UnselectSlot()
    {
        isSelected = false;
        SelectionFrame.SetActive(false);

    }

    public void SetNewCreatureImage(Image newCreatureImage)
    {
        CreatureImage = newCreatureImage;
        SelectionFrame.SetActive(false);
    }

    public void SetImageColor(Color newColor)
    {
        CreatureImage.color = newColor;
    }

    public void ResetColor()
    {
        CreatureImage.color = Color.white;
    }

    public void SetCreature(CreatureModel creature, bool isDefender)
    {
        GameObject CreatureUIPrefab = creatureCatalog.GetUIPrefab(creature.Name);
        CreatureImage = CreatureUIPrefab.GetComponent<Image>();

        Vector3 scale = CreatureImage.rectTransform.localScale;
        scale.x = isDefender ? -1f : 1f;
        CreatureImage.rectTransform.localScale = scale;
    }
}