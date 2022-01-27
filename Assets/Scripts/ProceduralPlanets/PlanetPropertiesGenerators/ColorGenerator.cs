using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProceduralPlanets {
    public class ColorGenerator {
        private ColorSettings m_ColorSettings;
        private Texture2D m_PlanetTexture;
        const int TEXTURE_RESOLUTION = 50;

        public void UpdateSettings(ref ColorSettings colorSettings) {
            this.m_ColorSettings = colorSettings;
            if(m_PlanetTexture == null) {
                m_PlanetTexture = new Texture2D(TEXTURE_RESOLUTION, 1);
            }
        }

        public void UpdateElevationColor(MinMax minMax) {
            m_ColorSettings.planetMaterial.SetVector("elevation_min_max", new Vector4(minMax.valueMin, minMax.valueMax));
        }

        public void UpdateColors() {
            Color[] colors = new Color[TEXTURE_RESOLUTION];
            for(int i = 0; i < TEXTURE_RESOLUTION; i++) {
                colors[i] = m_ColorSettings.planetGradient.Evaluate(i / ((float) TEXTURE_RESOLUTION - 1.0f));
            }

            m_PlanetTexture.SetPixels(colors);
            m_PlanetTexture.Apply();
            m_ColorSettings.planetMaterial.SetTexture("planet_texture", m_PlanetTexture);
        }
    }
}
