using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CursorBattlefieldController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cursorSprite;
    private CursorCatalog catalog;
    private CameraBattlefieldController battlefieldCamera;
    private BattlefieldController bfController;
    private ECursorType currentCursor;
    private TileController tileUnderCursor;


    public void Init(CursorCatalog newCursorCatalog, CameraBattlefieldController BattlefieldCamera, BattlefieldController newBattlefieldController)
    {
        catalog = newCursorCatalog;
        battlefieldCamera = BattlefieldCamera;
        bfController = newBattlefieldController;
        SetCursor(ECursorType.Default);
    }

    public void SetCursor(ECursorType type)
    {
        var sprite = catalog.GetCursorSprite(type);
        cursorSprite.sprite = sprite;
        currentCursor = type;
        transform.rotation = Quaternion.identity;
    }

    private void TryHandleTileClick()
    {
        if (tileUnderCursor == null) return;
        bfController.BfMouse.HandleClickTile(tileUnderCursor);
    }

    private bool RaycastToTile(out TileController tile)
    {
        int layerMask = ~LayerMask.GetMask("Ignore Raycast"); // ignorar uroboros Cursor
        Vector3 mouseWorldPos = battlefieldCamera.CameraComponent.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rayOrigin = new(mouseWorldPos.x, mouseWorldPos.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, 0f, layerMask);
        tile = null;

        if (!hit.collider) return false;

        tile = hit.collider.GetComponent<TileController>();
        return tile != null;
    }


    private void handleMouseMovent()
    {
        Vector3 mousePos = battlefieldCamera.CameraComponent.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }

    private void UpdateHoveredTile()
    {
        tileUnderCursor = RaycastToTile(out TileController tile) ? tile : null;
    }


    private void RotateMeleeCursorTowardMouse()
    {
        if (tileUnderCursor == null) return;

        Vector2 center = tileUnderCursor.Model.WorldPosition;
        Vector2 mouse = battlefieldCamera.CameraComponent.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mouse - center).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float snappedAngle = GetHexSnapAngle(angle);

        transform.rotation = Quaternion.Euler(0f, 0f, snappedAngle);
    }

    private float GetHexSnapAngle(float angle)
    {
        angle = (angle + 360f) % 360f;

        if (angle < 30f || angle >= 330f) return 90f; // E
        if (angle < 90f) return 150f;                 // NE
        if (angle < 150f) return 210f;                // NW
        if (angle < 210f) return 270f;                // W
        if (angle < 270f) return 330f;                // SW
        return 30f;                                   // SE
    }


    private void Update()
    {
        if (!battlefieldCamera.CameraComponent) return;
        handleMouseMovent();
        UpdateHoveredTile();

        if (currentCursor == ECursorType.MeleeAttack)
        {
            RotateMeleeCursorTowardMouse();
        }

        if (Input.GetMouseButtonDown(0))
        {
            TryHandleTileClick();
        }
    }
}
