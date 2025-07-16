using System;
using UnityEngine;

public class UIDeployController : MonoBehaviour, IUIDeployController
{
    public BattlefieldController bfController { get; private set; }
    public DeploymentPhase DeploymentPhaseController;
    public UIReservePanelController ReservePanelController;
    public FinishDeployButtonView FinishDeployButtonView;
    public Army Attacker { get; private set; }
    public Army Defender { get; private set; }
    public event Action<DeploySlotController> OnSlotClicked;
    public event Action OnFinishButtonclicked;


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
        OnFinishButtonclicked?.Invoke();
    }

    public void Init(BattlefieldController newBfController, DeploymentPhase newDeploymentPhasecontroller)
    {
        gameObject.SetActive(true);
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
        gameObject.SetActive(false);
    }
}