using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;

        }
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        worldPos.z = 0f;
        transform.position = worldPos;
    }
}
