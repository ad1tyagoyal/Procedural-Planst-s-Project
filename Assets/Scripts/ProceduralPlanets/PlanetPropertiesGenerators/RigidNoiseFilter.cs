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
            float noiseValue = 0.0f;
            float amplitude = 1.0f;
            float frequency = noiseSettings.baseLacunarity;
            float weight = noiseSettings.weight;

            for (int i = 0; i < noiseSettings.noOfOctaves; i++) {
                float tempNoise = 1 - Mathf.Abs(noise.Evaluate(point * frequency + noiseSettings.offset));
                tempNoise *= tempNoise;
                tempNoise *= weight;
                weight = tempNoise;

                noiseValue += tempNoise * amplitude;
                amplitude *= noiseSettings.persistence;
                frequency *= noiseSettings.lacunarity;
            }
            noiseValue = Mathf.Max(0.0f, (noiseValue - noiseSettings.minValue));
            return noiseValue * noiseSettings.strength;
        }
    }
}
