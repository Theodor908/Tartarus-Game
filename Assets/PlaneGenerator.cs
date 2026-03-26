using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Available before play mode
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class PlaneGenerator : MonoBehaviour
{
    [Header ("References")]
    Mesh myMesh;
    MeshFilter myMeshFilter;

    [Header ("Plane Settings")]
    [SerializeField] Vector2 planeSize = new Vector2(1, 1);
    [SerializeField] int planeResolution = 1;

    [Header("Mesh Values")]
    List<Vector3> vertices;
    List<int> triangles;

    private void Awake()
    {
        myMesh = new Mesh();
        myMeshFilter = GetComponent<MeshFilter>();
        myMeshFilter.mesh = myMesh;
    }

    private void Update()
    {
        
        planeResolution = Mathf.Clamp(planeResolution, 1, 100);

        GeneratePlane(planeSize, planeResolution);
        SineWave(Time.time);
        AssignMesh();

    }

    void GeneratePlane(Vector2 size, int resolution)
    {

        // Create vertices

        vertices = new List<Vector3>();
        float xPerStep = size.x / resolution;
        float yPerStep = size.y / resolution; 
    
        for(int y = 0; y <= resolution; y++) 
        {
            for(int x = 0; x <= resolution; x++) 
            {
                vertices.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
            }
        }

        // Create triangles

        triangles = new List<int>();

        for(int row = 0; row < resolution; row++)
        {
            for(int col = 0; col < resolution; col++)
            {
                int i = (row * resolution) + row + col;

                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);

                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);

            }
        }


    }

    void AssignMesh()
    {

        myMesh.Clear();
        myMesh.vertices = vertices.ToArray();
        myMesh.triangles = triangles.ToArray();
        myMesh.RecalculateNormals();

    }

    void SineWave(float time)
    {
        for(int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertex = vertices[i];
            vertex.y = Mathf.Sin(vertex.x + vertex.z + time);
            vertices[i] = vertex;
        }
    }   

}
