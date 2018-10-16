using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
class AddFinder : EditorWindow
{
    private float _buttonSize = 30.0f;

    private string tempString = "name";

    [MenuItem("Basic Manager/Object Finder")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        AddFinder window = (AddFinder)EditorWindow.GetWindow(typeof(AddFinder));
        window.Show();
    }

    void OnGUI()
    {
        if (EditorApplication.isPlaying) return;

        GUILayout.BeginHorizontal();
        GUILayout.Label("NAME", EditorStyles.boldLabel);
        tempString = GUILayout.TextField(tempString);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("INSTANCE", EditorStyles.boldLabel);
        if (Selection.activeGameObject)
            GUILayout.Label(Selection.activeGameObject.name);
        else
            GUILayout.Label("NONE");

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add Finder !", GUILayout.Height(_buttonSize)))
        {
            ObjectFinder._allFinders.Add(new ObjectFinder.AFinder(tempString, Selection.activeGameObject));
            Close();
        }
    }
}