using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PrefabDatabase", menuName = "Custom/Prefab Database")]
public class PrefabDatabase : ScriptableObject
{
    [System.Serializable]
    public class PrefabEntry
    {
        public string id;
        public GameObject prefab;
    }

    [SerializeField]
    private List<PrefabEntry> entries = new();

    private Dictionary<string, GameObject> prefabLookup;

    private void OnEnable()
    {
        prefabLookup = new Dictionary<string, GameObject>();

        foreach (var entry in entries)
        {
            if (string.IsNullOrWhiteSpace(entry.id))
            {
                Debug.LogWarning("Se encontró una entrada sin ID en el PrefabDatabase.");
                continue;
            }

            if (!prefabLookup.ContainsKey(entry.id))
            {
                prefabLookup.Add(entry.id, entry.prefab);
            }
            else
            {
                Debug.LogWarning($"ID duplicado en PrefabDatabase: {entry.id}");
            }
        }
    }

    public GameObject GetPrefabById(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogWarning("Se intentó obtener un prefab con un ID nulo o vacío.");
            return null;
        }

        if (prefabLookup.TryGetValue(id, out GameObject prefab))
        {
            return prefab;
        }

        Debug.LogWarning($"No se encontró ningún prefab con el ID: {id}");
        return null;
    }
}