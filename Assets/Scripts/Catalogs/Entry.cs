using System;
using UnityEngine;

[Serializable]
public class Entry : IEntry
{
    [SerializeField] private string name;
    public string Name => name;

}

[Serializable]
public class CreatureEntry : Entry, ICreatureEntry
{
    [SerializeField] private CreatureStats creatureStats;
    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;
    public CreatureStats CreatureStats => creatureStats;
}

[Serializable]
public class CreaturePrefabEntry : IEntry
{
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;

    public string Name => name;
    public GameObject Prefab => prefab;
}

public class CreatureDefinition
{
    public Creature Data { get; }
    public GameObject CombatPrefab { get; }
    public GameObject UIPrefab { get; }

    public CreatureDefinition(Creature data, GameObject combatPrefab, GameObject uiPrefab)
    {
        Data = data;
        CombatPrefab = combatPrefab;
        UIPrefab = uiPrefab;
    }
}

[Serializable]
public class CreatureCatalogEntry
{
    public ECreaturesNames CreatureId;
    public GameObject CombatPrefab;
    public GameObject UIPrefab;
}


