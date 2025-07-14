using UnityEngine;

public class DeploySlotView : MonoBehaviour
{
    public bool isSelected { get; private set; } = false;
    public GameObject SelectionFrame;

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
}