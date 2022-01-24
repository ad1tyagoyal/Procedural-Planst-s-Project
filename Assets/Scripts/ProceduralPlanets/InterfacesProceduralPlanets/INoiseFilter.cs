using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralPlanets {
    public interface INoiseFilter {
        public float Evaluate(ref Vector3 point);
    }
}
