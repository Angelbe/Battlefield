using UnityEngine;

public static class CreatureDB
{
    private static CreatureCatalog _catalog;

    private static CreatureCatalog Catalog
    {
        get
        {
            if (_catalog == null)
                _catalog = Resources.Load<CreatureCatalog>("Catalogs/CreatureCatalog");
            return _catalog;
        }
    }

    public static GameObject GetPrefab(string key) => Catalog.GetPrefab(key);
}
