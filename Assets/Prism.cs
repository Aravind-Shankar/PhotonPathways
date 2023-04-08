/*
using UnityEngine;

public class Prism : MonoBehaviour
{
    public float height = 1.0f;  // height of the prism
    public float radius = 1.0f;  // radius of the base of the prism
    public int numSides = 6;  // number of sides in the base of the prism
    public Material material;  // material for the prism

    void Start()
    {
        // create a new mesh
        Mesh mesh = new Mesh();

        // create vertices
        Vector3[] vertices = new Vector3[numSides * 2 * 3];
        float angleStep = 2 * Mathf.PI / numSides;
        for (int i = 0; i < numSides; i++)
        {
            float angle = angleStep * i;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);

            // bottom face
            vertices[i * 6 + 0] = new Vector3(x, 0, z);
            vertices[i * 6 + 1] = new Vector3(radius * Mathf.Cos(angle + angleStep), 0, radius * Mathf.Sin(angle + angleStep));
            vertices[i * 6 + 2] = new Vector3(0, 0, 0);

            // top face
            vertices[i * 6 + 3] = new Vector3(x, height, z);
            vertices[i * 6 + 4] = new Vector3(0, height, 0);
            vertices[i * 6 + 5] = new Vector3(radius * Mathf.Cos(angle + angleStep), height, radius * Mathf.Sin(angle + angleStep));
        }

        // create triangles
        int[] triangles = new int[numSides * 2 * 3];
        for (int i = 0; i < numSides * 6; i++)
        {
            triangles[i] = i;
        }

        // assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // create a mesh filter and renderer
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

        // assign the mesh to the filter and set the material of the renderer
        filter.mesh = mesh;
        renderer.material = material;
    }
}
*/

using UnityEngine;

public class Prism : MonoBehaviour
{
    public float height = 1.0f;  // height of the prism
    public float radius = 1.0f;  // radius of the base of the prism
    public int numSides = 6;  // number of sides in the base of the prism
    public Material material;  // material for the prism

    void Start()
    {
        // create a new mesh
        Mesh mesh = new Mesh();

        // create vertices
        Vector3[] vertices = new Vector3[numSides * 2 * 3 + numSides * 4];
        float angleStep = 2 * Mathf.PI / numSides;
        for (int i = 0; i < numSides; i++)
        {
            float angle = angleStep * i;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);

            // bottom face
            vertices[i * 6 + 0] = new Vector3(x, 0, z);
            vertices[i * 6 + 1] = new Vector3(radius * Mathf.Cos(angle + angleStep), 0, radius * Mathf.Sin(angle + angleStep));
            vertices[i * 6 + 2] = new Vector3(0, 0, 0);

            // top face
            vertices[i * 6 + 3] = new Vector3(x, height, z);
            vertices[i * 6 + 4] = new Vector3(0, height, 0);
            vertices[i * 6 + 5] = new Vector3(radius * Mathf.Cos(angle + angleStep), height, radius * Mathf.Sin(angle + angleStep));

            // sides
            vertices[numSides * 6 + i * 4 + 0] = vertices[i * 6 + 0];
            vertices[numSides * 6 + i * 4 + 1] = vertices[i * 6 + 3];
            vertices[numSides * 6 + i * 4 + 2] = vertices[(i * 6 + 3) % (numSides * 6)];
            vertices[numSides * 6 + i * 4 + 3] = vertices[(i * 6 + 1) % (numSides * 6)];
        }

        // create triangles
        int[] triangles = new int[numSides * 2 * 3 + numSides * 2 * 3];
        for (int i = 0; i < numSides * 6; i++)
        {
            triangles[i] = i;
        }

        // create triangles for sides
        for (int i = 0; i < numSides; i++)
        {
            triangles[numSides * 6 + i * 6 + 0] = numSides * 6 + i * 4 + 0;
            triangles[numSides * 6 + i * 6 + 1] = numSides * 6 + i * 4 + 1;
            triangles[numSides * 6 + i * 6 + 2] = numSides * 6 + (i * 4 + 2) % (numSides * 4) + numSides * 6;
            triangles[numSides * 6 + i * 6 + 3] = numSides * 6 + i * 4 + 2;
            triangles[numSides * 6 + i * 6 + 4] = numSides * 6 + i * 4 + 3;
            triangles[numSides * 6 + i * 6 + 5] = numSides * 6 + (i * 4 + 0) % (numSides * 4) + numSides * 6;
        }
            // assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // create a mesh filter and renderer
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

        // assign the mesh to the filter and set the material of the renderer
        filter.mesh = mesh;
        renderer.material = material;
}
}