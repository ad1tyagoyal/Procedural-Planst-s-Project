using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPlanets {

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
            Vector3[] _vertices = new Vector3[(int) Mathf.Pow(m_Resolution, 2)];
            int[] _triangleIndices = new int[(int)(Mathf.Pow((m_Resolution - 1), 2) * 6)];

            int _verticesIndex = 0, _triangleIndex = 0;

            //foreach vertices
            for (int y = 0; y < m_Resolution; y++) {
                for (int x = 0; x < m_Resolution; x++) {

                    Vector2 _percentage = new Vector2(x, y) / (m_Resolution - 1);
                    Vector3 _pointOnUnitCube = m_LocalUp + ((_percentage.x - 0.5f) * 2 * m_AxisA)
                                                        + ((_percentage.y - 0.5f) * 2 * m_AxisB);
                    _pointOnUnitCube.Normalize();

                    _vertices[_verticesIndex] = m_ShapeGenerator.GetPointOnPlanet(ref _pointOnUnitCube);

                    if (x != (m_Resolution - 1) && y != (m_Resolution - 1)) {
                        _triangleIndices[_triangleIndex] = _verticesIndex;
                        _triangleIndices[_triangleIndex + 1] = _verticesIndex + 1;
                        _triangleIndices[_triangleIndex + 2] = _verticesIndex + m_Resolution + 1;
                        
                        _triangleIndices[_triangleIndex + 3] = _verticesIndex;
                        _triangleIndices[_triangleIndex + 4] = _verticesIndex + m_Resolution + 1;
                        _triangleIndices[_triangleIndex + 5] = _verticesIndex + m_Resolution;

                        _triangleIndex += 6;
                    }

                    _verticesIndex++;
                }
            }

            m_Mesh.Clear();
            m_Mesh.vertices = _vertices;
            m_Mesh.triangles = _triangleIndices;
            m_Mesh.RecalculateNormals();
        }
    }
}
