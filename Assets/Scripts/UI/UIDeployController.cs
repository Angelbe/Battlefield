using System;
using UnityEngine;

public class UIDeployController : MonoBehaviour
{
    public ReservePanelController ReservePanelController;
    public DeploySlotController SlotSelected;
    public Army Attacker { get; private set; }
    public Army Defender { get; private set; }
    public event Action<CreatureStack> OnSlotSelected;


    public void EnableUI()
    {
        enabled = true;
    }

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

    public void SetSlotSelected(DeploySlotController newSlotSelected)
    {
        if (SlotSelected != null)
        {
            ClearSlotSelected();
        }
        SlotSelected = newSlotSelected;
        OnSlotSelected?.Invoke(newSlotSelected.Model.StackInTheSlot);
    }

    public void ClearSlotSelected()
    {
        SlotSelected.UnselectSlot();
    }

    public void Init(BattlefieldController bfController)
    {
        Attacker = bfController.bfModel.Attacker;
        Defender = bfController.bfModel.Defender;
        ShowAttackerDeploy();
    }
}