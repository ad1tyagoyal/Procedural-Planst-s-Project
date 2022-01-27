using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProceduralPlanets {
    public class SimplexNoiseFilter : INoiseFilter {
        SimplexPerlinNoise noise = new SimplexPerlinNoise();
        NoiseSettings.SimplexNoiseFilterSettings noiseSettings;

        public SimplexNoiseFilter(NoiseSettings.SimplexNoiseFilterSettings settings) {
            this.noiseSettings = settings;
        }

        public float Evaluate(ref Vector3 point) {
            float _noiseValue = 0;
            float _amplitude = 1;
            float _frequency = noiseSettings.baseLacunarity;

            for(int i = 0; i < noiseSettings.noOfOctaves; i++) {
                float _tempNoise = noise.Evaluate(point * _frequency + noiseSettings.offset);
                _noiseValue += (_tempNoise + 1) * 0.5f * _amplitude;
                _amplitude *= noiseSettings.persistence;
                _frequency *= noiseSettings.lacunarity;
            }
            _noiseValue = Mathf.Max(0.0f, (_noiseValue - noiseSettings.minValue));
            return _noiseValue * noiseSettings.strength;
        }

    }

}
