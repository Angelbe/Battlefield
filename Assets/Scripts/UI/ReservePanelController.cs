using System;
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
    private Dictionary<Guid, DeploySlotController> slotControllers = new();
    public IReadOnlyDictionary<Guid, DeploySlotController> SlotControllers => slotControllers;

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
        // Limpia hijos visuales
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Limpia el diccionario de controllers
        slotControllers.Clear();

        if (ReserveToShow == null) return;

        foreach (CreatureStack creatureStack in ReserveToShow.CreaturesInReserve.Values)
        {
            var deploySlotModel = new DeploySlotModel(creatureStack);
            var slotGO = Instantiate(DeploySlotPrefab, transform);
            var deploySlotController = slotGO.GetComponent<DeploySlotController>();
            deploySlotController.Init(deploySlotModel, UIDeployController);

            // Añadir al diccionario usando el ID del stack
            slotControllers[creatureStack.ID] = deploySlotController;
        }
    }

    public DeploySlotController GetSlotByID(Guid IDToSearch)
    {
        return slotControllers[IDToSearch];
    }


}