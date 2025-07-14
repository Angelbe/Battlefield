using UnityEngine;

public class UIDeployController : MonoBehaviour
{
    public ReservePanelController ReservePanelController;
    public Army Attacker { get; private set; }
    public Army Defender { get; private set; }


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

    public void Init(BattlefieldController bfController)
    {
        Attacker = bfController.bfModel.Attacker;
        Defender = bfController.bfModel.Defender;
        ShowAttackerDeploy();
    }
}