using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeploySlotController : MonoBehaviour
{
    public DeploySlotView View;
    public DeploySlotModel Model { get; private set; }
    public TextMeshProUGUI QuantityText;
    public UIDeployController UIDeployController;
    // public CreatureController CreatureController { get; private set; }
    [SerializeField] private CreatureCatalog CreatureCatalog;

    public void InstantiateNewCreature(ECreaturesNames creatureName)
    {
        GameObject CreaturePrefabGO = CreatureCatalog.GetUIPrefab(creatureName);
        if (CreaturePrefabGO == null)
        {
            Debug.LogWarning($"[DeploySlotController] ⚠ No se Pudo instanciar el deploy slot de '{creatureName}'");
            return;
        }
        GameObject CreaturePrefabUI = Instantiate(CreaturePrefabGO, transform);
    }

    private void OnMouseDown()
    {
        View.SelectSlot();
        UIDeployController.SetSlotSelected(this);
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