using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ScriptableObjects;
using PlanetPropertiesGenerators;


namespace PlanetGenerationScripts {

    public class PlanetScript : MonoBehaviour {

        [Range(2, 256)]
        public int resolution = 10;

        [SerializeField, HideInInspector]
        private MeshFilter[] m_MeshFilters;
        private TerrainFace[] m_TerrainFaces;

        [SerializeField]
        private ShapeGenerator m_ShapeGenerator;


        [SerializeField] private ShapeSettings m_ShapeSettings;
        [SerializeField] private ColorSettings m_ColorSettings;


        private void OnValidate() {
            GeneratePlanet();
        }

        private void GeneratePlanet() {
            InitializeMeshes();
            GeneratePlanetMesh();
            GeneratePlanetColors();
        }
        private void OnShapeSettingsUpdates() {
            InitializeMeshes();
            GeneratePlanetMesh();
        }

        private void OnColorSettingsUpdated() {
            InitializeMeshes();
            GeneratePlanetColors();
        }



        private void InitializeMeshes() {
            m_ShapeGenerator = new ShapeGenerator(ref m_ShapeSettings);

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
                m_TerrainFaces[i] = new TerrainFace(ref m_ShapeGenerator, resolution, m_MeshFilters[i].sharedMesh, directions[i]);
            }
        }


        private void GeneratePlanetMesh() {
            foreach(TerrainFace terrainFace in m_TerrainFaces) {
                terrainFace.ConstructMesh();
            }
        }

        private void GeneratePlanetColors() {
            foreach (MeshFilter meshFilter in m_MeshFilters) {
                meshFilter.GetComponent<MeshRenderer>().sharedMaterial.color = m_ColorSettings.planetColor;
            }
        }

    }
}
