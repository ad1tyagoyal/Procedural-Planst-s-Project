namespace ProceduralPlanets {
    public static class NoiseFilterFactory {
        public static INoiseFilter CreateNoiseFilter(NoiseSettings settings) {
            switch(settings.noiseFilterType) {
                case NoiseSettings.FilterType.Simplex:
                    return new SimplexNoiseFilter(settings.simplexNoiseFilterSettings);
                case NoiseSettings.FilterType.Rigid:
                    return new RigidNoiseFilter(settings.rigidNoiseFilterSettings);
            }
            return null;
        }
    }

}

