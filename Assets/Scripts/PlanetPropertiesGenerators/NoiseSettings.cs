using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings {
    [Range(1, 8)]
    [SerializeField] int noOfOctaves = 1;
    [SerializeField] float strength = 1;
    [SerializeField] float baseLacunarity = 1;
    [SerializeField] float lacunarity = 1;
    [SerializeField] float persistence = 1;
    [SerializeField] Vector3 offset = new Vector3( 1.0f, 1.0f , 1.0f);
    [SerializeField] float minValue = 1;

    public int NoOfOctaves {
        get => this.noOfOctaves;
        set => this.noOfOctaves = value;
    }
    
    public float Strength {
        get => this.strength; 
        set => this.strength = value;
    }
    
    public float BaseLacunarity {
        get => this.baseLacunarity;
        set => this.baseLacunarity = value;
    }
    
    public float Lacunarity {
        get => this.lacunarity; 
        set => this.lacunarity = value;
    }
    
    public float Persistence {
        get => this.persistence;
        set => this.persistence = value;
    }
    
    public Vector3 Offset {
        get => this.offset;
        set => this.offset = value;
    }
    
    public float MinValue{
        get => this.minValue;
        set => this.minValue = value;
    }
}

