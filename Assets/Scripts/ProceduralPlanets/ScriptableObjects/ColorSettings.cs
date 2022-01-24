using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPlanets {

    [CreateAssetMenu(fileName = "New Color Settings", menuName = "Scriptable Objects / Color Settings")]
    public class ColorSettings : ScriptableObject {
        public Gradient planetGradient;
        public Material planetMaterial;
    }
}

