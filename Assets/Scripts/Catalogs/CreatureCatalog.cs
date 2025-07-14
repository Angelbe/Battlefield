using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Catalog Creatures", menuName = "Uroboros/Catalog/CreatureCatalog")]
public class CreatureCatalog : ScriptableObject
{
    [SerializeField] private TextAsset creatureJson;
    [SerializeField] private List<CreatureCatalogEntry> entries = new();

    private Dictionary<ECreaturesNames, CreatureDefinition> creatures = new();

    public Dictionary<ECreaturesNames, CreatureDefinition> Creatures => creatures;

    private void OnEnable()
    {

        creatures.Clear();

        var wrapper = JsonUtility.FromJson<CreatureCatalogWrapper>(creatureJson.text);
        if (wrapper == null || wrapper.creatures == null) return;

        // Convertir las criaturas desde JSON y mapearlas por nombre string
        var creatureDataByName = new Dictionary<ECreaturesNames, Creature>();

        foreach (var dto in wrapper.creatures)
        {
            var creature = dto.ToModel();
            if (creature == null) continue; // ❌ Ignora si el nombre no fue parseado

            creatureDataByName[creature.Name] = creature;
        }

        foreach (CreatureCatalogEntry entry in entries)
        {

            if (!creatureDataByName.TryGetValue(entry.CreatureId, out var creatureData))
            {
                Debug.LogWarning($"[CreatureCatalog] No se encontró data en el JSON para: {entry.CreatureId}");
                continue;
            }

            creatures[entry.CreatureId] = new CreatureDefinition(creatureData, entry.CombatPrefab, entry.UIPrefab);
        }
    }

    public bool HasEntry(ECreaturesNames id)
    => creatures.ContainsKey(id);

    private CreatureDefinition GetDefinition(ECreaturesNames id)
    {
        if (creatures.TryGetValue(id, out var def)) return def;

        Debug.LogWarning($"[CreatureCatalog] ⚠ No se encontró entrada para '{id}'");
        return null;
    }

    public Creature GetCreatureData(ECreaturesNames id)
        => GetDefinition(id)?.Data;

    public GameObject GetCombatPrefab(ECreaturesNames id)
        => GetDefinition(id)?.CombatPrefab;

    public GameObject GetUIPrefab(ECreaturesNames id)
        => GetDefinition(id)?.UIPrefab;
}
