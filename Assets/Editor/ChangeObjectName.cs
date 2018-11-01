using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
class ChangeObjectName : EditorWindow
{
    private float _buttonSize = 30.0f;
    private string _original = "";
    private string _replacement = "";
    private string _prefix = "";

    [MenuItem("Tools/Change Object Name")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GetWindow(typeof(ChangeObjectName)).Show();
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying) return;

        //GUILayout.BeginHorizontal();
        GUILayout.Label("Original", EditorStyles.boldLabel);
        _original = GUILayout.TextField(_original);
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        GUILayout.Label("Replacement", EditorStyles.boldLabel);
        _replacement = GUILayout.TextField(_replacement);
        //GUILayout.EndHorizontal();

        GameObject selected = Selection.activeGameObject;
        if (GUILayout.Button("REPLACE !", GUILayout.Height(_buttonSize)) && selected != null)
        {
            ReplaceName(selected.transform);
        }

        GUILayout.Label("Prefix", EditorStyles.boldLabel);
        _prefix = GUILayout.TextField(_prefix);

        selected = Selection.activeGameObject;
        if (GUILayout.Button("ADD !", GUILayout.Height(_buttonSize)) && selected != null)
        {
            AddPrefix(selected.transform);
        }
    }

    private void ReplaceName(Transform t)
    {
        t.name = t.name.Replace(_original, _replacement);
        for(int i = 0; i < t.childCount; i++)
        {
            ReplaceName(t.GetChild(i));
        }
    }

    private void AddPrefix(Transform t)
    {
        t.name = _prefix + t.name;
        for (int i = 0; i < t.childCount; i++)
        {
            AddPrefix(t.GetChild(i));
        }
    }
}