using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralPlane : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 nodeScale;


    private Mesh mesh;

    private void OnValidate()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "Procedural Grid";
            GetComponent<MeshFilter>().mesh = mesh;
        }

        Vector3[] vertices = new Vector3[(gridSize.x + 1) * (gridSize.y + 1)];
        for (int i = 0, y = 0; y <= gridSize.y; y++)
        {
            for (int x = 0; x <= gridSize.x; x++, i++)
            {
                vertices[i] = new Vector3(x * nodeScale.x, 0, y * nodeScale.y);
            }
        }

        mesh.vertices = vertices;

        int[] triangles = new int[gridSize.x * gridSize.y * 6];
        for (int ti = 0, vi = 0, y = 0; y < gridSize.y; y++, vi++)
        {
            for (int x = 0; x < gridSize.x; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + gridSize.x + 1;
                triangles[ti + 5] = vi + gridSize.x + 2;
            }
        }

        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
