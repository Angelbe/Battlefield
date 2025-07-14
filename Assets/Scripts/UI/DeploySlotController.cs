using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DeploySlotController : MonoBehaviour
{
    public DeploySlotModel CreatureStackModel { get; set; }

    public void Init(DeploySlotModel ModelToShow)
    {
        CreatureStackModel = ModelToShow;
    }
}