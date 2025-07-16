using UnityEngine;

public class UICombatController : MonoBehaviour
{
    public void Init()
    {
        gameObject.SetActive(true);
    }
    public void Shutdown()
    {
        gameObject.SetActive(false);
    }
}