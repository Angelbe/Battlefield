using UnityEngine;

public class GhostCreatureHandler
{
    private GameObject currentGhost;
    private Transform parent;
    private CreatureCatalog creatureCatalog;

    public GhostCreatureHandler(Transform parentTransform, CreatureCatalog catalog)
    {
        parent = parentTransform;
        creatureCatalog = catalog;
    }

    public void ShowGhost(CreatureStack creatureStack, TileController tileController)
    {
        HideGhost();

        var prefab = creatureCatalog.GetCombatPrefab(creatureStack.Creature.Name);
        if (prefab == null) return;

        currentGhost = GameObject.Instantiate(prefab, parent);
        currentGhost.transform.position = tileController.Model.WorldPosition; // Suponiendo método util
        currentGhost.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // semitransparente
    }

    public void HideGhost()
    {
        if (currentGhost != null)
            GameObject.Destroy(currentGhost);
    }
}
