using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProceduralPlanets {

    public class PlanetScript : MonoBehaviour {

        [Range(2, 256)]
        public int resolution = 10;

        [SerializeField, HideInInspector]
        private MeshFilter[] m_MeshFilters;
        private TerrainFace[] m_TerrainFaces;

        [SerializeField]
        private ShapeGenerator m_ShapeGenerator = new ShapeGenerator();
        private ColorGenerator m_ColorGenerator = new ColorGenerator();

        [SerializeField] private ShapeSettings m_ShapeSettings;
        [SerializeField] private ColorSettings m_ColorSettings;

        public void GeneratePlanet() {
            InitializeMeshes();
            GeneratePlanetMesh();
            GeneratePlanetColors();
        }
        public void OnShapeSettingsUpdates() {
            InitializeMeshes();
            GeneratePlanetMesh();
        }

        public void OnColorSettingsUpdated() {
            InitializeMeshes();
            GeneratePlanetColors();
        }



        private void InitializeMeshes() {
            m_ShapeGenerator.UpdateSettings(ref m_ShapeSettings);
            m_ColorGenerator.UpdateSettings(ref m_ColorSettings);

            if(m_MeshFilters == null || m_MeshFilters.Length == 0)  
                m_MeshFilters = new MeshFilter[6];

            m_TerrainFaces = new TerrainFace[6];

            Vector3[] _directions = { Vector3.up, 
                                     Vector3.down, 
                                     Vector3.right, 
                                     Vector3.left, 
                                     Vector3.back, 
                                     Vector3.forward };

            for(int i = 0; i < 6; i++) {
                if(m_MeshFilters[i] == null) {
                    GameObject _faceMesh = new GameObject("Face Mesh" + i);
                    _faceMesh.transform.parent = transform;
                    _faceMesh.AddComponent<MeshRenderer>();
                    m_MeshFilters[i] = _faceMesh.AddComponent<MeshFilter>();
                    m_MeshFilters[i].sharedMesh = new Mesh();
                }
                m_TerrainFaces[i] = new TerrainFace(ref m_ShapeGenerator, resolution, m_MeshFilters[i].sharedMesh, _directions[i]);
            }
        }


        private void GeneratePlanetMesh() {
            foreach(TerrainFace _terrainFace in m_TerrainFaces) {
                _terrainFace.ConstructMesh();
            }
            Debug.Log(m_ShapeGenerator.elevationMinMax.valueMin + " " + m_ShapeGenerator.elevationMinMax.valueMax);
            m_ColorGenerator.UpdateElevationColor(m_ShapeGenerator.elevationMinMax);
        }

        private void GeneratePlanetColors() {
            m_ColorGenerator.UpdateColors();
        }

        public ref ShapeSettings GetShapeSettings() { return ref m_ShapeSettings; }
        public ref ColorSettings GetColorSettings() { return ref m_ColorSettings; }

    }
}
