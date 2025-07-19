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
        currentGhost = GameObject.Instantiate(creatureCatalog.GetCombatPrefab(creatureStack.Creature.Name), GhostGO);
        if (currentGhost == null) return;
        //SIEMPRE instancia el prefab antes de modificarlo
        CreatureController crController = currentGhost.GetComponent<CreatureController>();
        if (crController.IsDefender != creatureStack.IsDefender) crController.SetAsDefender(creatureStack.IsDefender);
        currentGhost.transform.position = tileController.Model.WorldPosition;

        var spriteRenderer = currentGhost.transform.Find("Creature")?.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            Debug.LogWarning($"[GhostHandler] No se encontró SpriteRenderer en hijo 'Creature' del prefab {creatureStack.Creature.Name}");
        }
    }


    public void HideGhost()
    {
        if (currentGhost != null)
            GameObject.Destroy(currentGhost);
    }
}
