using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ParallaxManager), true)]
public class ParallaxManagerEditor : Editor
{

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        float buttonWidth = 250;
        GUILayout.Space(Screen.width / 2 - buttonWidth / 2);
        if (GUILayout.Button("Regenerate parallax gameobjects", GUILayout.Width(buttonWidth), GUILayout.Height(50)))
        {
            ParallaxManager PM = (ParallaxManager)target;
            PM.GenerateParallaxGameobjects();
            EditorUtility.SetDirty(PM);
        }
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
