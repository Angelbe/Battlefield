using UnityEngine;

public interface IEntry
{
    string Name { get; }
    GameObject Prefab { get; }
}

public interface ICreatureEntry : IEntry
{
    CreatureStats CreatureStats { get; }
}
