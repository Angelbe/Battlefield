using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureCatalog", menuName = "Uroboros/CreatureCatalog")]
public class CreatureCatalog : ScriptableObject
{
    [Serializable]
    public struct Entry
    {
        public string Name;           // Name
        public GameObject Prefab;       // variant
    }
    public List<Entry> entries;
    public GameObject GetPrefab(string Name)
        => entries.Find(e => e.Name == Name).Prefab;
}
