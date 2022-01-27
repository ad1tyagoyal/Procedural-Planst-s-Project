using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


using ProceduralPlanets;

[CustomEditor(typeof(PlanetScript))]
public class PlanetEditor : Editor {
    private PlanetScript m_Planet;

    private Editor m_ShapeEditor;
    private Editor m_ColorEditor;

    public  override void OnInspectorGUI() {
        using (var _check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();
            if (_check.changed) m_Planet.GeneratePlanet();
        }
        DrawPlanetSettings(m_Planet.GetColorSettings(), m_Planet.OnColorSettingsUpdated, ref m_ColorEditor);
        DrawPlanetSettings(m_Planet.GetShapeSettings(), m_Planet.OnShapeSettingsUpdates, ref m_ShapeEditor);
    }

    private void DrawPlanetSettings(Object settings, System.Action OnSettingsUpdated, ref Editor editor) {
        if (!settings) return;

        using (var _check = new EditorGUI.ChangeCheckScope()) {
            EditorGUILayout.InspectorTitlebar(true, settings);
            CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();
            if (_check.changed)
                OnSettingsUpdated();
        }
    }

    private void OnEnable() {
        m_Planet = (PlanetScript)target; 
    }


}
