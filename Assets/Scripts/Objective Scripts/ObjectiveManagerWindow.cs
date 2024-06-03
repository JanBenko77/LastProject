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

        if (Selection.activeTransform != null)
        {
            target = Selection.activeTransform.gameObject;
        }
        else
        {
            target = null;
        }


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
        GameObject newObject = new GameObject("Objective");
        newObject.AddComponent<ObjectiveTarget>();
        Selection.activeObject = newObject;
        Undo.RegisterCreatedObjectUndo(newObject, "Create New Objective");
    }

    private void CreateNewObjectiveItem()
    {
        var go = PrefabUtility.InstantiatePrefab(itemPrefab);
        go.GetComponent<ObjectiveItem>().ObjectiveType = target.GetComponent<ObjectiveTarget>().requiredType;
        Undo.RegisterCreatedObjectUndo(itemPrefab, "Create New Objective Item");
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