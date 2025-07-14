using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Catalog Champions", menuName = "Uroboros/Catalogs/Champion")]
public class ChampionCatalog : Catalog
{
    [SerializeField] private List<IEntry> entries;
}

