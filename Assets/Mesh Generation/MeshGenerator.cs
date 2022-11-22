using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    MeshFilter _mf;
    MeshRenderer _mr;
    Mesh _mesh;

    Vector3[] _vertices;
    int[] _tris;

    Vector2[] _UV;
    Vector3[] _normals;

    void Awake()
    {
        _mf = GetComponent<MeshFilter>();
        _mr = GetComponent<MeshRenderer>();
        _mr.sharedMaterial = new Material(Shader.Find("Standard"));
    }

    void Start()
    {
        //CreateQuad(transform.position, 1, 1, true);
    }

    public void CreateQuads(Vector3[] worldPositions)
    {
        float width = 1f;
        float height = 1f;
        float hWidth = width/2;
        float hHeight = height/2;

        // 4 verts for every quad
        int vertCount = 4 * worldPositions.Length;
        _vertices = new Vector3[vertCount];

        // 2 tris for every quad, 3 indexes per tri
        int triCount = 2 * worldPositions.Length * 3;
        _tris = new int[triCount];

        // Create UV
        _UV = new Vector2[vertCount];

        // Create normals
        _normals = new Vector3[vertCount];

        // Loop through worldPositions
        for(int i = 0; i < worldPositions.Length; i++)
        {
            float randomHeight = Random.Range(0, 3);
            randomHeight = randomHeight/2;
            // Assign verts
            _vertices[i * 4 + 0] = new Vector3(-hWidth + worldPositions[i].x, randomHeight, -hHeight + worldPositions[i].z);
            _vertices[i * 4 + 1] = new Vector3(hWidth + worldPositions[i].x, randomHeight, -hHeight + worldPositions[i].z);
            _vertices[i * 4 + 2] = new Vector3(-hWidth + worldPositions[i].x, randomHeight, hHeight + worldPositions[i].z);
            _vertices[i * 4 + 3] = new Vector3(hWidth + worldPositions[i].x, randomHeight, hHeight + worldPositions[i].z);
            //Debug.Log("i: " + i + ", x: " + worldPositions[i].x + ", y: " + worldPositions[i].z);

            // Assign tris
            // lower left triangle
            _tris[i * 6 + 0] = i*4;
            _tris[i * 6 + 1] = i*4+2;
            _tris[i * 6 + 2] = i*4+1;
            // upper right triangle
            _tris[i * 6 + 3] = i*4+2;
            _tris[i * 6 + 4] = i*4+3;
            _tris[i * 6 + 5] = i*4+1;
            
            // Assign normals
            _normals[i * 4 + 0] = Vector3.up;
            _normals[i * 4 + 1] = Vector3.up;
            _normals[i * 4 + 2] = Vector3.up;
            _normals[i * 4 + 3] = Vector3.up;

            // Assign UVs
            _UV[i * 4 + 0] = new Vector2(i+0, i+0);
            _UV[i * 4 + 1] = new Vector2(i+1, i+0);
            _UV[i * 4 + 2] = new Vector2(i+0, i+1);
            _UV[i * 4 + 3] = new Vector2(i+1, i+1);
        }

        /*
        _tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        */
        Debug.Log(_tris[0] + ", " + _tris[1] + ", " + _tris[2] + ", " + _tris[3] + ", " + _tris[4] + ", " + _tris[5]);
        
        // Create mesh
        CreateMesh();
    }

    public void CreateQuad(Vector3 worldPos, float width, float height, bool centered)
    {
        if(width <= 0 | height <= 0)
        {
            Debug.LogWarning("Width or height are negative");
            return;
        }

        // Create verts
        if(!centered)
        {
            _vertices = new Vector3[4]
            {
                new Vector3(0, 0, 0),
                new Vector3(width, 0, 0),
                new Vector3(0, height, 0),
                new Vector3(width, height, 0)
            };
        }else{
            float hWidth = width/2;
            float hHeight = height/2;
            _vertices = new Vector3[4]
            {
                new Vector3(-hWidth, -hHeight, 0),
                new Vector3(hWidth, -hHeight, 0),
                new Vector3(-hWidth, hHeight, 0),
                new Vector3(hWidth, hHeight, 0)
            };
        }

        // Adjust to given world position
        for(int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i] += worldPos;
        }

        // Create triangles
        _tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };

        // Create UV
        _UV = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        // Create normals
        _normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };

        // Create mesh
        CreateMesh();
    }

    private void CreateMesh()
    {
        _mesh = new Mesh();
        _mf.mesh = _mesh;
        _mesh.vertices = _vertices;
        _mesh.triangles = _tris;

        _mesh.uv = _UV;
        _mesh.normals = _normals;
        
    }

    void OnDrawGizmos()
    {
        if(_mesh == null || _mesh.vertices.Length <= 0)
        {
            return;
        }

        for(int i = 0; i < _mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(_mesh.vertices[i], .1f);
        }
    }
}
