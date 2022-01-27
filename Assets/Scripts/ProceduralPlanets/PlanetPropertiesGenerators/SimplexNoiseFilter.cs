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
            float noiseValue = 0;
            float amplitude = 1;
            float frequency = noiseSettings.baseLacunarity;

            for(int i = 0; i < noiseSettings.noOfOctaves; i++) {
                float tempNoise = noise.Evaluate(point * frequency + noiseSettings.offset);
                noiseValue += (tempNoise + 1) * 0.5f * amplitude;
                amplitude *= noiseSettings.persistence;
                frequency *= noiseSettings.lacunarity;
            }
            noiseValue = Mathf.Max(0.0f, (noiseValue - noiseSettings.minValue));
            return noiseValue * noiseSettings.strength;
        }

    }

}
