using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ScriptableObjects;

namespace PlanetPropertiesGenerators {
    
    public class ShapeGenerator {
        private ShapeSettings m_ShapeSettings;

        public ShapeGenerator(ref ShapeSettings shapeSettings) {
            m_ShapeSettings = shapeSettings;
        }

        public ref Vector3 GetPointOnPlanet(ref Vector3 pointOnUnitSphere) {
            pointOnUnitSphere *= m_ShapeSettings.planetRadius;
            return ref pointOnUnitSphere;
        }
    
    }

}



