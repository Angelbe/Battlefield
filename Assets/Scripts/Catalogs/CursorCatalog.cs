using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Uroboros/Catalogs/CursorCatalog")]
public class CursorCatalog : ScriptableObject
{
    [Serializable]
    public struct CursorEntry
    {
        public ECursorType Type;
        public Sprite Sprite;
    }

    public GameObject CursorPrefab;
    [SerializeField] private List<CursorEntry> cursorEntries;

    private Dictionary<ECursorType, Sprite> lookup;

    private void OnEnable()
    {
        lookup = new();
        foreach (CursorEntry entry in cursorEntries)
            lookup[entry.Type] = entry.Sprite;
    }

    public Sprite GetCursorSprite(ECursorType type)
    {
        return lookup.TryGetValue(type, out Sprite sprite) ? sprite : null;
    }
}

public enum ECursorType
{
    Default,
    Move,
    MeleeAttack,
    RangedAttack,
    Forbidden
}
