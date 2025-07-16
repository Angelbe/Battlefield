using UnityEngine;
using UnityEngine.UI;

public class FinishDeployButtonView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;

    public void SetEnabledState(bool isEnabled)
    {
        button.interactable = isEnabled;
        buttonImage.color = isEnabled ? Color.white : Color.gray;
    }

    public void handleClick()
    {
        Debug.Log("Clicked");
    }

}
