using UnityEngine;

public class GhostCreatureHandler
{
    private GameObject currentGhost;
    private Transform GhostGO;
    private CreatureCatalog creatureCatalog;

    public GhostCreatureHandler(Transform newGhostGO, CreatureCatalog catalog)
    {
        GhostGO = newGhostGO;
        creatureCatalog = catalog;
    }

    public void ShowGhost(CreatureStack creatureStack, TileController tileController)
    {
        var prefab = creatureCatalog.GetCombatPrefab(creatureStack.Creature.Name);
        if (prefab == null) return;

        currentGhost = GameObject.Instantiate(prefab, GhostGO);
        currentGhost.transform.position = tileController.Model.WorldPosition;

        var spriteRenderer = currentGhost.transform.Find("Creature")?.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            Debug.LogWarning($"[GhostHandler] No se encontró SpriteRenderer en hijo 'Creature' del prefab {prefab.name}");
        }
    }


    public void HideGhost()
    {
        if (currentGhost != null)
            GameObject.Destroy(currentGhost);
    }
}
