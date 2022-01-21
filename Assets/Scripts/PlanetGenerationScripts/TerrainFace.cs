using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetPropertiesGenerators;

namespace PlanetGenerationScripts {

    public class TerrainFace {

        private ShapeGenerator m_ShapeGenerator;
        private int m_Resolution;
        private Mesh m_Mesh;
        private Vector3 m_LocalUp, m_AxisA, m_AxisB;

        public TerrainFace(ref ShapeGenerator shapeGenerator, int resolution, Mesh mesh, Vector3 localUp) {
            m_ShapeGenerator = shapeGenerator;
            m_Resolution = resolution;
            m_Mesh = mesh;
            m_LocalUp = localUp;
            m_AxisA = new Vector3(m_LocalUp.y, m_LocalUp.z, m_LocalUp.x);
            m_AxisB = Vector3.Cross(m_LocalUp, m_AxisA);
        }

        public void ConstructMesh() {
            Vector3[] vertices = new Vector3[(int) Mathf.Pow(m_Resolution, 2)];
            int[] triangleIndices = new int[(int)(Mathf.Pow((m_Resolution - 1), 2) * 6)];

            {
                int verticesIndex = 0, triangleIndex = 0;

                //foreach vertices
                for(int y = 0; y < m_Resolution; y++) {
                    for(int x = 0; x < m_Resolution; x++) {
                
                        Vector2 percentage = new Vector2(x, y) / (m_Resolution - 1);
                        Vector3 pointOnUnitCube = m_LocalUp + ((percentage.x - 0.5f) * 2 * m_AxisA) 
                                                            + ((percentage.y - 0.5f) * 2 * m_AxisB);
                        pointOnUnitCube.Normalize();

                        vertices[verticesIndex] = m_ShapeGenerator.GetPointOnPlanet(ref pointOnUnitCube);
               
                        if(x != (m_Resolution - 1) && y != (m_Resolution - 1)) {
                            triangleIndices[triangleIndex]          = verticesIndex; 
                            triangleIndices[triangleIndex + 1]      = verticesIndex + 1; 
                            triangleIndices[triangleIndex + 2]      = verticesIndex + m_Resolution + 1; 
                        
                            triangleIndices[triangleIndex + 3]      = verticesIndex; 
                            triangleIndices[triangleIndex + 4]      = verticesIndex + m_Resolution + 1; 
                            triangleIndices[triangleIndex + 5]      = verticesIndex +m_Resolution; 
                        
                            triangleIndex += 6;
                        }

                        verticesIndex++;
                    }
                }
            }

            m_Mesh.Clear();
            m_Mesh.vertices = vertices;
            m_Mesh.triangles = triangleIndices;
            m_Mesh.RecalculateNormals();
        }
    }
}
