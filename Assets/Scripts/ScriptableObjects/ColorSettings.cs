using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {

    [CreateAssetMenu(fileName = "New Color Settings", menuName = "Scriptable Objects / Color Settings")]
    public class ColorSettings : ScriptableObject {
        public Color planetColor;
    }
}

