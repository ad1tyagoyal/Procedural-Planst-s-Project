using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPlanets {
    public class RigidNoiseFilter : INoiseFilter {
        SimplexPerlinNoise noise = new SimplexPerlinNoise();
        NoiseSettings.RigidNoiseFilterSettings noiseSettings;

        public RigidNoiseFilter(NoiseSettings.RigidNoiseFilterSettings settings) {
            this.noiseSettings = settings;
        }

        public float Evaluate(ref Vector3 point) {
            float _noiseValue = 0.0f;
            float _amplitude = 1.0f;
            float _frequency = noiseSettings.baseLacunarity;
            float _weight = noiseSettings.weight;

            for (int i = 0; i < noiseSettings.noOfOctaves; i++) {
                float _tempNoise = 1 - Mathf.Abs(noise.Evaluate(point * _frequency + noiseSettings.offset));
                _tempNoise *= _tempNoise;
                _tempNoise *= _weight;
                _weight = _tempNoise;

                _noiseValue += _tempNoise * _amplitude;
                _amplitude *= noiseSettings.persistence;
                _frequency *= noiseSettings.lacunarity;
            }
            _noiseValue = Mathf.Max(0.0f, (_noiseValue - noiseSettings.minValue));
            return _noiseValue * noiseSettings.strength;
        }
    }
}
