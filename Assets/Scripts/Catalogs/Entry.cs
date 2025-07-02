using System;
using UnityEngine;

[Serializable]
public class Entry : IEntry
{
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;
    public string Name => name;
    public GameObject Prefab => prefab;


}

[Serializable]
public class CreatureEntry : Entry, ICreatureEntry
{
    [SerializeField] private CreatureStats creatureStats;

    public CreatureStats CreatureStats => creatureStats;
}

