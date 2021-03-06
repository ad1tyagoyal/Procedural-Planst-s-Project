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

        [SerializeField] private ShapeSettings[] m_ShapeSettings;
        [SerializeField] private ColorSettings[] m_ColorSettings;
        
        
        private ShapeSettings m_ShapeSetting;
        private ColorSettings m_ColorSetting;



        void Start()
        {
            GenerateRandomPlanet();
        }

        public void GenerateRandomPlanet() {
            m_ShapeSetting = m_ShapeSettings[Random.Range(0, m_ShapeSettings.Length)];
            m_ColorSetting = m_ColorSettings[Random.Range(0, m_ColorSettings.Length)];
            GeneratePlanet();
        }






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
            m_ShapeGenerator.UpdateSettings(ref m_ShapeSetting);
            m_ColorGenerator.UpdateSettings(ref m_ColorSetting);

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
                    faceMesh.AddComponent<MeshRenderer>();
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
            m_ColorGenerator.UpdateElevationColor(m_ShapeGenerator.elevationMinMax);
        }

        private void GeneratePlanetColors() {
            /*foreach (MeshFilter meshFilter in m_MeshFilters) {
                meshFilter.GetComponent<MeshRenderer>().sharedMaterial = m_ColorSettings.planetMaterial;
            }*/
            m_ColorGenerator.UpdateColors();
        }

        public ref ShapeSettings GetShapeSettings() { return ref m_ShapeSetting; }
        public ref ColorSettings GetColorSettings() { return ref m_ColorSetting; }

    }
}
