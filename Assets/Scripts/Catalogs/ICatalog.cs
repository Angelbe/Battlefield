using System.Collections.Generic;
using UnityEngine;

public interface ICatalog<TEntry> where TEntry : IEntry
{
    List<TEntry> Entries { get; }
    GameObject GetPrefab(string name);
    TEntry GetEntry(string name);
}
