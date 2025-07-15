using System;
using UnityEngine;

public interface IUIDeployController
{
    public DeploySlotController SlotSelected { get; set; }
    public Army Attacker { get; }
    public Army Defender { get; }
    public event Action<DeploySlotController> OnSlotSelected;
    public event Action OnSlotUnselected;
}

public class UIDeployController : MonoBehaviour, IUIDeployController
{
    public BattlefieldController bfController;
    public ReservePanelController ReservePanelController;
    public DeploySlotController SlotSelected { get; set; }
    public Army Attacker { get; private set; }
    public Army Defender { get; private set; }
    public event Action<DeploySlotController> OnSlotSelected;
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

    public void HandleDeployslotSelected(DeploySlotController slotClicked)
    {
        bfController.BfSpawn.HandleSlotClicked(slotClicked);
    }

    public void Init(BattlefieldController newBfController)
    {
        enabled = true;
        bfController = newBfController;
        Attacker = bfController.bfModel.Attacker;
        Defender = bfController.bfModel.Defender;
        ShowAttackerDeploy();
    }

    public void Shutdown()
    {
        Attacker = null;
        Defender = null;
        enabled = false;
    }
}