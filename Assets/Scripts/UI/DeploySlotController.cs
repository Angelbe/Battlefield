using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeploySlotController : MonoBehaviour
{
    public DeploySlotView View;
    public DeploySlotModel Model { get; private set; }
    public TextMeshProUGUI QuantityText;
    public UIDeployController UIDeployController;
    [SerializeField] private CreatureCatalog CreatureCatalog;

    public void InstantiateNewCreature(ECreaturesNames creatureName)
    {
        GameObject CreaturePrefabGO = CreatureCatalog.GetUIPrefab(creatureName);
        if (CreaturePrefabGO == null)
        {
            // Debug.LogWarning($"[DeploySlotController] ⚠ No se Pudo instanciar el deploy slot de '{creatureName}'");
            return;
        }
        GameObject CreaturePrefabUI = Instantiate(CreaturePrefabGO, transform);
    }

    private void OnMouseDown()
    {
        UIDeployController.HandleSlotClicked(this);
    }

    public void SlotSelected()
    {
        View.SelectSlot();
    }

    public void UnselectSlot()
    {
        View.UnselectSlot();
    }


    public void Init(DeploySlotModel ModelToShow, UIDeployController uIDeployController)
    {
        UIDeployController = uIDeployController;
        Model = ModelToShow;
        QuantityText.text = ModelToShow.StackInTheSlot.Quantity.ToString();
        InstantiateNewCreature(Model.StackInTheSlot.Creature.Name);
        View.UnselectSlot();
    }


}