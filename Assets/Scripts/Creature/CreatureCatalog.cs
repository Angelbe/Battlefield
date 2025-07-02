using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureCatalog", menuName = "Uroboros/CreatureCatalog")]
public class CreatureCatalog : Catalog
{
    public List<CreatureEntry> entries;
}