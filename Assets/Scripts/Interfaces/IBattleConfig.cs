using UnityEngine;

public interface IBattlefieldConfig
{
    int Rows { get; set; }
    int Cols { get; set; }
    float HexSize { get; set; }
    GameObject tilePrefab { get; set; }
    GameObject battlefieldPrefab { get; set; }
    TextAsset deploymentZonesJson { get; set; }
}