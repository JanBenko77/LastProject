using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using Unity.VisualScripting;
#if UNITY_EDITOR

public class ObjectiveManagerWindow : EditorWindow
{
    [MenuItem("Tools/Objective Editor")]

    public static void Open()
    {
        ObjectiveManagerWindow window = GetWindow<ObjectiveManagerWindow>();
        window.titleContent = new GUIContent("Objective Manager");
    }

    private GameObject target;

    [SerializeField] private GameObject itemPrefab;

    private void OnSelectionChange()
    {
        Repaint();
    }

    private void OnGUI()
    {
        GUILayout.Label("Objective Manager", EditorStyles.boldLabel);

        target = Selection.activeTransform != null ? Selection.activeTransform.gameObject : null;


        if (target != null)
        {
            if (target.GetComponent<ObjectiveTarget>() != null)
            {
                EditorGUILayout.LabelField("Selected Objective: ", target.name);
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();
                DrawUI();
                EditorGUILayout.EndVertical();
            }
            else
            {
                if (GUILayout.Button("Create New Objective"))
                {
                    CreateNewObjective();
                }
            }
            if (target.GetComponent<ObjectiveItem>() != null)
            {
                EditorGUILayout.LabelField("Selected Objective Item: ", target.name);
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();
                DrawUIItem();
                EditorGUILayout.EndVertical();
            }
        }
        else
        {
            if (GUILayout.Button("Create New Objective"))
            {
                CreateNewObjective();
            }
        }
    }


    private void CreateNewObjective()
    {
        GameObject newObject = new("Objective");
        newObject.AddComponent<ObjectiveTarget>();
        Selection.activeObject = newObject;
        Undo.RegisterCreatedObjectUndo(newObject, "Create New Objective");
    }

    private void CreateNewObjectiveItem()
    {
        Object go = PrefabUtility.InstantiatePrefab(itemPrefab);
        GameObject goGo = go as GameObject;
        goGo.transform.position = target.transform.position;
        go.GetComponent<ObjectiveItem>().ObjectiveType = target.GetComponent<ObjectiveTarget>().requiredType;
        Undo.RegisterCreatedObjectUndo(itemPrefab, "Create New Objective Item");
    }

    private void DrawUIItem()
    {
        EditorGUILayout.LabelField("Objective Item Settings", EditorStyles.boldLabel);
        EditorGUILayout.ObjectField("Objective Item Script", target.GetComponent<ObjectiveItem>(), typeof(ObjectiveItem), true);
        Editor editorTarget = Editor.CreateEditor(target.GetComponent<ObjectiveItem>());
        editorTarget.OnInspectorGUI();

        EditorGUILayout.Space();

        if (GUILayout.Button("Delete Objective Item"))
        {
            DestroyImmediate(target);
        }
    }

    private void DrawUI()
    {
        EditorGUILayout.LabelField("Objective Settings", EditorStyles.boldLabel);
        EditorGUILayout.ObjectField("Objective Script", target.GetComponent<ObjectiveTarget>(), typeof(ObjectiveTarget), true);
        Editor editorTarget = Editor.CreateEditor(target.GetComponent<ObjectiveTarget>());
        editorTarget.OnInspectorGUI();

        EditorGUILayout.Space();

        if (target.GetComponent<ObjectiveTarget>().FindObjectiveItemOfSameType() != null)
        {
            EditorGUILayout.LabelField("Objective Item", EditorStyles.boldLabel);
            EditorGUILayout.ObjectField("Objective Item Script", target.GetComponent<ObjectiveTarget>().FindObjectiveItemOfSameType(), typeof(ObjectiveItem), true);
            Editor editorItem = Editor.CreateEditor(target.GetComponent<ObjectiveTarget>().FindObjectiveItemOfSameType());
            editorItem.OnInspectorGUI();
        }
        else
        {
            EditorGUILayout.LabelField("Objective Item", EditorStyles.boldLabel);
            if (GUILayout.Button("Create New Objective Item"))
            {
                CreateNewObjectiveItem();
            }
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Delete Objective"))
        {
            DestroyImmediate(target);
        }
    }
}
#endif