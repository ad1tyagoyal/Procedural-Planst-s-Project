using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {

    [Range(2, 256)]
    public int resolution = 10;

    [SerializeField, HideInInspector]
    private MeshFilter[] m_MeshFilters;
    private TerrainFace[] m_TerrainFaces;

    private void OnValidate() {
        InitializeMeshes();
        GeneratePlanetMesh();
    }

    private void InitializeMeshes() {
        
        if(m_MeshFilters == null || m_MeshFilters.Length == 0)  
            m_MeshFilters = new MeshFilter[6];

        m_TerrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, 
                                 Vector3.down, 
                                 Vector3.right, 
                                 Vector3.left, 
                                 Vector3.back, 
                                 Vector3.forward };

        for(int i = 0; i < 6; i++) {
            if(m_MeshFilters[i] == null) {
                GameObject faceMesh = new GameObject("Face Mesh" + i);
                faceMesh.transform.parent = transform;

                faceMesh.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                m_MeshFilters[i] = faceMesh.AddComponent<MeshFilter>();
                m_MeshFilters[i].sharedMesh = new Mesh();
            }
            m_TerrainFaces[i] = new TerrainFace(resolution, m_MeshFilters[i].sharedMesh, directions[i]);
        }
    }

    private void GeneratePlanetMesh() {
        foreach(TerrainFace terrainFace in m_TerrainFaces) {
            terrainFace.ConstructMesh();
        }
    }

}
