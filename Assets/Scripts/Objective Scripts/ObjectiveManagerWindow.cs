using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR

public class ObjectiveManagerWindow : EditorWindow
{
    [MenuItem("Tools/Objective Editor")]
    

    public static void OpenWindow()
    {
        ObjectiveManagerWindow window = (ObjectiveManagerWindow)GetWindow(typeof(ObjectiveManagerWindow));
        window.titleContent = new GUIContent("Objective Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Objective Manager", EditorStyles.boldLabel);
        if (GUILayout.Button("Create New Objective"))
        {
            CreateNewObjective();
        }
    }

    private void CreateNewObjective()
    {
        //Create new game object with ObjectiveTarget component
        GameObject obj = new GameObject("Objective");
        obj.AddComponent<ObjectiveTarget>();
    }
}
#endif