using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings {
    [System.Serializable]
    public enum FilterType { Simplex, Rigid }
    [System.Serializable]
    public class SimplexNoiseFilterSettings {
        [Range(1, 8)]
        public int noOfOctaves = 1;
        public float strength = 1;
        public float baseLacunarity = 1;
        public float lacunarity = 1;
        public float persistence = 1;
        public Vector3 offset = new Vector3(1.0f, 1.0f, 1.0f);
        public float minValue = 1;
    }
    [System.Serializable]
    public class RigidNoiseFilterSettings : SimplexNoiseFilterSettings {
        public float weight = 1.0f;
    }

    public FilterType noiseFilterType;
    public SimplexNoiseFilterSettings simplexNoiseFilterSettings;
    public RigidNoiseFilterSettings rigidNoiseFilterSettings;
    

}

