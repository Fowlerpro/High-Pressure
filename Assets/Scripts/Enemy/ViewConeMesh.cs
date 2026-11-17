using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ViewConeMesh : MonoBehaviour
{
    public float distance = 5f;
    public float width = 4f;

    void Start()
    {
        CreateMesh();
    }

    void CreateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[3];

        // Point near the enemy
        vertices[0] = new Vector3(0f, 0f, 0f);

        // Far left
        vertices[1] = new Vector3(-width / 2f, 0f, distance);

        // Far right
        vertices[2] = new Vector3(width / 2f, 0f, distance);

        int[] triangles = new int[]
        {
            0, 1, 2
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}