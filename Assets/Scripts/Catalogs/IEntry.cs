using UnityEngine;

public interface IEntry
{
    string Name { get; }
}

public interface ICreatureEntry : IEntry
{
    CreatureStats CreatureStats { get; }
    GameObject Prefab { get; }

}

