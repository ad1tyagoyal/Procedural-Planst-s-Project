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
            float _elevation = 0.0f;
            float _firstLayerValue = 0.0f;

            if(m_NoiseFilters.Length > 0) {
                _firstLayerValue = m_NoiseFilters[0].Evaluate(ref pointOnUnitSphere);
                if (m_ShapeSettings.noiseLayers[0].enable) {
                    _elevation = _firstLayerValue;
                }
            }


            for (int i = 1; i < m_NoiseFilters.Length; i++) {
                if(m_ShapeSettings.noiseLayers[i].enable) {
                    float _mask = (m_ShapeSettings.noiseLayers[i].usingFirstLayerAsMask) ? _firstLayerValue : 1;
                    _elevation += m_NoiseFilters[i].Evaluate(ref pointOnUnitSphere) * _mask;
                }
            }

            float _finalElevation = (m_ShapeSettings.planetRadius * (1 + _elevation));
            elevationMinMax.UpdateMinMax(_finalElevation);
            pointOnUnitSphere *= _finalElevation;

            return ref pointOnUnitSphere;
        }
    
    }

}



