using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "CameraBattlefieldConfig", menuName = "Uroboros/CameraConfig")]
public class CameraBattlefieldConfig : ScriptableObject
{
     [Header("Values")]
    public Vector3 initialPosition = new Vector3(0f, 0f, -10f);
    public int assetsPPU = 32;
    public int refResolutionX = 640;
    public int refResolutionY = 360;
    [Header("Prefabs")]
    public GameObject CameraPrefab;
}
