using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UtilsProceduralPlanets;

namespace PlanetPropertiesGenerators {
    public class NoiseFilter {
        SimplexPerlinNoise noise = new SimplexPerlinNoise();
        NoiseSettings noiseSettings;

        public NoiseFilter(NoiseSettings settings) {
            this.noiseSettings = settings;
        }

        public float Evaluate(ref Vector3 point) {
            float noiseValue = 0;
            float amplitude = 1;
            float frequency = noiseSettings.BaseLacunarity;

            for(int i = 0; i < noiseSettings.NoOfOctaves; i++) {
                float tempNoise = noise.Evaluate(point * frequency + noiseSettings.Offset);
                noiseValue += (tempNoise + 1) * 0.5f * amplitude;
                amplitude *= noiseSettings.Persistence;
                frequency *= noiseSettings.Lacunarity;
            }
            noiseValue = Mathf.Max(0.0f, (noiseValue - noiseSettings.MinValue));
            return noiseValue * noiseSettings.Strength;
        }

    }

}
