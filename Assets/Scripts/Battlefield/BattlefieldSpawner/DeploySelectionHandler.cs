using UnityEngine;

public class DeploySelectionHandler
{
    private CreatureStack selectedStack;
    private DeploySlotController selectedSlot;

    public bool HasSelection => selectedStack != null;
    public CreatureStack SelectedStack => selectedStack;

    public void HandleSlotClicked(DeploySlotController clickedSlot)
    {
        if (selectedSlot == null)
        {
            Select(clickedSlot);
            return;
        }

        if (selectedSlot.Model.CreatureStack.ID == clickedSlot.Model.CreatureStack.ID)
        {
            Unselect();
            return;
        }

        Unselect();
        Select(clickedSlot);
    }

    private void Select(DeploySlotController slot)
    {
        selectedSlot = slot;
        selectedSlot.SlotSelected();
        selectedStack = slot.Model.CreatureStack;
    }

    public void Unselect()
    {
        if (selectedSlot != null)
            selectedSlot.UnselectSlot();

        selectedSlot = null;
        selectedStack = null;
    }

    public void ConfirmDeployment()
    {
        if (selectedSlot == null) return;

        selectedSlot.UnselectSlot();
        selectedSlot.SlotDeployed();
        selectedSlot = null;
        selectedStack = null;
    }
}
