
namespace ProceduralPlanets {
    public class MinMax {
        public float valueMin { get; private set; }
        public float valueMax { get; private set; }

        public MinMax() {
            valueMin = float.MaxValue;
            valueMax = float.MinValue;
        }

        public void UpdateMinMax(float value) {
            valueMax = (valueMax < value) ? value : valueMax;
            valueMin = (valueMin > value) ? value : valueMin;
        }
    }

}
