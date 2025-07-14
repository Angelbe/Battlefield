using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Catalog : ScriptableObject, ICatalog<IEntry>
{
    public List<IEntry> Entries { get; private set; } = new();

    public IEntry GetEntry(string name)
        => Entries.FirstOrDefault(e => e.Name == name);
}
