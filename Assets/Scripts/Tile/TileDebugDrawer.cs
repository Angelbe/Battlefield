#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BattlefieldGizmoDrawer : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private BattlefieldController bfController;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (bfController == null)
        {
            Debug.LogWarning("Battlefield Not initialized for Gizmos");
            enabled = false;
        }

        if (bfController == null || bfController.BfGrid.TilesInTheBattlefield == null)
            return;

        foreach (TileController tileController in bfController.BfGrid.TilesInTheBattlefield.Values)
        {
            ColRow colRow = tileController.Model.ColRow;
            CubeCoord coord = tileController.Model.Coord;
            float size = bfController.BfConfig.HexSize;
            Vector3 pos = ColRow.FromColRowToWorldPosition(colRow, size);

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
