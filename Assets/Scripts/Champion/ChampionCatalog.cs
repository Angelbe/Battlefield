using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChampionCatalog",
                 menuName = "Uroboros/Champion Catalog")]
public class ChampionCatalog : Catalog
{
    [SerializeField] private List<IEntry> entries;
}

