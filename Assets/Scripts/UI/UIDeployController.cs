using System;
using UnityEngine;

public interface IUIDeployController
{
    public DeploySlotController SlotSelected { get; set; }
    public Army Attacker { get; }
    public Army Defender { get; }
    public event Action<CreatureStack> OnSlotSelected;
    public event Action OnSlotUnselected;
}

public class UIDeployController : MonoBehaviour, IUIDeployController
{
    public ReservePanelController ReservePanelController;
    public DeploySlotController SlotSelected { get; set; }
    public Army Attacker { get; private set; }
    public Army Defender { get; private set; }
    public event Action<CreatureStack> OnSlotSelected;
    public event Action OnSlotUnselected;


    public void DisableUI()
    {
        enabled = false;
    }

    public void ShowAttackerDeploy()
    {
        ReservePanelController.ShowNewReserve(Attacker.Reserve);
    }
    public void ShowDefenderDeploy()
    {
        ReservePanelController.ShowNewReserve(Defender.Reserve);
    }

    public void HandleSlotClicked(DeploySlotController slotClicked)
    {
        if (SlotSelected == null)
        {
            SetSlotSelected(slotClicked);
            return;
        }
        if (SlotSelected.Model.StackInTheSlot.ID == slotClicked.Model.StackInTheSlot.ID)
        {
            ClearSlotSelected();
            return;
        }
        ClearSlotSelected();
        SetSlotSelected(slotClicked);
    }

    public void SetSlotSelected(DeploySlotController newSlotSelected)
    {
        SlotSelected = newSlotSelected;
        SlotSelected.SlotSelected();
        OnSlotSelected?.Invoke(newSlotSelected.Model.StackInTheSlot);
    }

    public void ClearSlotSelected()
    {
        SlotSelected.UnselectSlot();
        SlotSelected = null;
        OnSlotUnselected?.Invoke();
    }

    public void Init(BattlefieldController bfController)
    {
        enabled = true;
        Attacker = bfController.bfModel.Attacker;
        Defender = bfController.bfModel.Defender;
        ShowAttackerDeploy();
    }

    public void Shutdown()
    {
        Attacker = null;
        Defender = null;
        ClearSlotSelected();
        enabled = false;
    }
}