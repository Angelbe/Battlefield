using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CursorBattlefieldController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cursorSprite;
    private CursorCatalog catalog;
    private CameraBattlefieldController battlefieldCamera;

    public void Init(CursorCatalog newCursorCatalog, CameraBattlefieldController BattlefieldCamera)
    {
        catalog = newCursorCatalog;
        battlefieldCamera = BattlefieldCamera;
        SetCursor(ECursorType.Default);
    }

    public void SetCursor(ECursorType type)
    {
        var sprite = catalog.GetCursorSprite(type);
        cursorSprite.sprite = sprite;
    }

    private void Update()
    {
        if (!battlefieldCamera.CameraComponent) return;
        Vector3 mousePos = battlefieldCamera.CameraComponent.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }
}
