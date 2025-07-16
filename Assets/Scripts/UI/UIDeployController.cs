using System;
using UnityEngine;

public interface IUIDeployController
{
    public Army Attacker { get; }
    public Army Defender { get; }
    public event Action<DeploySlotController> OnSlotClicked;
}

public class UIDeployController : MonoBehaviour, IUIDeployController
{
    public BattlefieldController bfController { get; private set; }
    public DeploymentPhaseController DeploymentPhaseController;
    public UIReservePanelController ReservePanelController;
    public FinishDeployButtonView FinishDeployButtonView;
    public Army Attacker { get; private set; }
    public Army Defender { get; private set; }
    public event Action<DeploySlotController> OnSlotClicked;


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

    public void HandleDeploySlotSelected(DeploySlotController slotClicked)
    {
        OnSlotClicked?.Invoke(slotClicked);
    }

    public void HandleFinishButtonClicked()
    {
        if (bfController.ActiveArmy == Attacker)
        {
            HandleAttackerFinishDeploy();
        }
    }

    public void HandleAttackerFinishDeploy()
    {

        DeploymentPhaseController.StartDefenderDeployment();
    }

    public void Init(BattlefieldController newBfController, DeploymentPhaseController newDeploymentPhasecontroller)
    {
        enabled = true;
        bfController = newBfController;
        DeploymentPhaseController = newDeploymentPhasecontroller;
        Attacker = bfController.bfModel.Attacker;
        Defender = bfController.bfModel.Defender;
        ShowAttackerDeploy();
        FinishDeployButtonView.SetEnabledState(true);
    }

    public void Shutdown()
    {
        Attacker = null;
        Defender = null;
        enabled = false;
    }
}