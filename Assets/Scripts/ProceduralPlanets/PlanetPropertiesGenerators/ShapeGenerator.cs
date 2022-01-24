using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPlanets {
    
    public class ShapeGenerator {
        public MinMax elevationMinMax;

        [SerializeField] INoiseFilter[] m_NoiseFilters;
        private ShapeSettings m_ShapeSettings;

        public void UpdateSettings(ref ShapeSettings shapeSettings) {
            elevationMinMax = new MinMax();
            m_ShapeSettings = shapeSettings;
            m_NoiseFilters = new INoiseFilter[m_ShapeSettings.noiseLayers.Length];
            for (int i = 0; i < m_NoiseFilters.Length; i++)
                m_NoiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(m_ShapeSettings.noiseLayers[i].noiseSettings);
        }

        public ref Vector3 GetPointOnPlanet(ref Vector3 pointOnUnitSphere) {
            float elevation = 0.0f;
            float firstLayerValue = 0.0f;

            if(m_NoiseFilters.Length > 0) {
                firstLayerValue = m_NoiseFilters[0].Evaluate(ref pointOnUnitSphere);
                if (m_ShapeSettings.noiseLayers[0].enable) {
                    elevation = firstLayerValue;
                }
            }


            for (int i = 1; i < m_NoiseFilters.Length; i++) {
                if(m_ShapeSettings.noiseLayers[i].enable) {
                    float mask = (m_ShapeSettings.noiseLayers[i].usingFirstLayerAsMask) ? firstLayerValue : 1;
                    elevation += m_NoiseFilters[i].Evaluate(ref pointOnUnitSphere) * mask;
                }
            }

            float finalElevation = (m_ShapeSettings.planetRadius * (1 + elevation));
            elevationMinMax.UpdateMinMax(finalElevation);
            pointOnUnitSphere *= finalElevation;

            return ref pointOnUnitSphere;
        }
    
    }

}



