using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IDeploySlotController
{
    public DeploySlotModel Model { get; }

    public UIDeployController UIDeployController { get; }
}

public class DeploySlotController : MonoBehaviour, IDeploySlotController
{
    public DeploySlotView View;
    public bool isDeployed;
    public TextMeshProUGUI QuantityText;
    public DeploySlotModel Model { get; private set; }
    public UIDeployController UIDeployController { get; private set; }
    public GameObject CreatureUIGO { get; set; }
    [SerializeField] private CreatureCatalog CreatureCatalog;

    public void UpdateModelInTheSlot(DeploySlotModel newDeployModel)
    {
        Model = newDeployModel;
        SetNewCreatureUI();
        UpdateQuantityText(Model.CreatureStack.Quantity);
    }

    public void SetNewCreatureUI()
    {
        GameObject CreatureUIPrefab = CreatureCatalog.GetUIPrefab(Model.CreatureStack.Creature.Name);
        if (CreatureUIPrefab == null)
        {
            // Debug.LogWarning($"[DeploySlotController] ⚠ No se Pudo instanciar el deploy slot de '{creatureName}'");
            return;
        }
        Destroy(CreatureUIGO);
        GameObject newGO = Instantiate(CreatureUIPrefab, transform);
        CreatureUIGO = newGO;
        Image ImageFromCreatureUI = newGO.GetComponent<Image>();
        View.SetNewCreatureImage(ImageFromCreatureUI);
    }

    public void UpdateQuantityText(int newQuantity)
    {
        QuantityText.text = newQuantity.ToString();
    }

    public void SlotSelected()
    {
        View.SelectSlot();
        View.SetImageColor(UroborosColors.HighlightedColor);
    }

    public void UnselectSlot()
    {
        View.UnselectSlot();
        View.ResetColor();
    }
    private void OnMouseDown()
    {
        if (isDeployed)
        {
            return;
        }
        UIDeployController.HandleDeployslotSelected(this);
    }

    public void SlotDeployed()
    {
        isDeployed = true;
        View.SetImageColor(UroborosColors.DimmedColor);
    }

    public void SlotUndeployed()
    {
        isDeployed = false;
        View.ResetColor();
    }

    public void Init(DeploySlotModel ModelToShow, UIDeployController uIDeployController)
    {
        UIDeployController = uIDeployController;
        Model = ModelToShow;
        UpdateModelInTheSlot(Model);
        View.UnselectSlot();
    }


}