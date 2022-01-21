using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {

    [CreateAssetMenu(fileName = "New Shape Settings", menuName = "Scriptable Objects / Shape Settings")]
    public class ShapeSettings : ScriptableObject {
        public float planetRadius = 1.0f;
    }

}

