#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BattlefieldGizmoDrawer : MonoBehaviour
{
#if UNITY_EDITOR
    private BattlefieldController battlefield;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (battlefield == null)
            battlefield = GetComponent<BattlefieldController>();

        if (battlefield == null || battlefield.TileCtrls == null)
            return;

        foreach (var pair in battlefield.TileCtrls)
        {
            var coord = pair.Key;
            float size = battlefield.bfConfig.HexSize;
            Vector3 pos = TileUtils.CubeToWorld(coord, size);

            string label = $"{coord.X},{coord.Y},{coord.Z}";

            GUIStyle style = new GUIStyle
            {
                normal = { textColor = Color.white },
                fontSize = 11, // más pequeño
                alignment = TextAnchor.MiddleCenter
            };

            // Centrado: compensar la altura de la fuente
            Vector3 offset = new Vector3(0, 0.02f, 0); // ligero desplazamiento vertical si hace falta
            Handles.Label(pos + offset, label, style);
        }
    }
#endif
}
