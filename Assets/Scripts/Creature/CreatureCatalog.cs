using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureCatalog", menuName = "Uroboros/CreatureCatalog")]
public class CreatureCatalog : ScriptableObject
{
    [SerializeField] private TextAsset creatureJson;

    public Dictionary<string, Creature> CreaturesByName = new();

    private void OnEnable()
    {
        CreaturesByName.Clear();

        var wrapper = JsonUtility.FromJson<CreatureCatalogWrapper>(creatureJson.text);
        if (wrapper == null || wrapper.creatures == null) return;

        foreach (var dto in wrapper.creatures)
        {
            Creature creature = dto.ToModel();
            CreaturesByName[dto.name] = creature;
        }

    }


    public Creature Get(string name)
    {
        return CreaturesByName[name];
    }
}
