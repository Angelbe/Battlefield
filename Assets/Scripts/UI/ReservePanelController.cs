using System.Collections.Generic;
using UnityEngine;

public interface IReservePanelController
{
    public ReserveHandler ReserveToShow { get; set; }
    public void ShowNewReserve(ReserveHandler newReserve);
    public void ClearReserve();
}

public class ReservePanelController : MonoBehaviour, IReservePanelController
{
    public UIDeployController UIDeployController;
    public GameObject DeploySlotPrefab;
    public ReserveHandler ReserveToShow { get; set; }

    public void ShowNewReserve(ReserveHandler newReserve)
    {
        ReserveToShow = newReserve;
        HandleReserveChanged();
    }

    public void ClearReserve()
    {
        ReserveToShow = null;
    }

    private void HandleReserveChanged()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (ReserveToShow == null) return;

        foreach (CreatureStack creatureStack in ReserveToShow.CreaturesInReserve.Values)
        {
            DeploySlotModel DeploySlotModel = new DeploySlotModel(creatureStack);
            GameObject slotGO = Instantiate(DeploySlotPrefab, transform);
            DeploySlotController DeploySlotController = slotGO.GetComponent<DeploySlotController>();
            DeploySlotController.Init(DeploySlotModel, UIDeployController);
        }
    }

}