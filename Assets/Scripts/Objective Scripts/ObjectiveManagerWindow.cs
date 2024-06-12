using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.Windows;
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
            if (target.GetComponentInChildren<ObjectiveTarget>() != null)
            {
                EditorGUILayout.LabelField("Selected Objective: ", target.name);
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();
                DrawUI();
                EditorGUILayout.EndVertical();
            }
            else if (target.GetComponentInChildren<ObjectiveItem>() != null)
            {
                EditorGUILayout.LabelField("Selected Objective Item: ", target.name);
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();
                DrawUIItem();
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
        GameObject newObject = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Objectives/ObjectiveTargetPrefab.prefab");
        if (newObject != null)
        {
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(newObject);
            go.GetComponentInChildren<ObjectiveTarget>().requiredType = ObjectiveType.None;
            Selection.activeObject = go;
            if (target != null)
            {
                go.transform.parent = target.transform;
            }
            Undo.RegisterCreatedObjectUndo(go, "Create New Objective");
        }
        else
        {
            Debug.LogError("Prefab not found");
        }
    }

    private void CreateNewObjectiveItem()
    {
        GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Objectives/ObjectiveItemPrefab.prefab");
        if (gameObject != null)
        {
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(gameObject);
            go.transform.position = target.transform.position;
            go.GetComponentInChildren<ObjectiveItem>().ObjectiveType = target.GetComponentInChildren<ObjectiveTarget>().requiredType;
            Undo.RegisterCreatedObjectUndo(go, "Create New Objective Item");
        }
        else
        {
            Debug.LogError("Prefab not found");
        }
    }

    private void DrawUIItem()
    {
        EditorGUILayout.LabelField("Objective Item Settings", EditorStyles.boldLabel);
        EditorGUILayout.ObjectField("Objective Item Script", target.GetComponentInChildren<ObjectiveItem>(), typeof(ObjectiveItem), true);
        Editor editorTarget = Editor.CreateEditor(target.GetComponentInChildren<ObjectiveItem>());
        editorTarget.OnInspectorGUI();

        EditorGUILayout.Space();

        if (GUILayout.Button("Delete Objective Item"))
        {
            Undo.DestroyObjectImmediate(target);
        }
    }

    private void DrawUI()
    {
        EditorGUILayout.LabelField("Objective Settings", EditorStyles.boldLabel);
        EditorGUILayout.ObjectField("Objective Script", target.GetComponentInChildren<ObjectiveTarget>(), typeof(ObjectiveTarget), true);
        Editor editorTarget = Editor.CreateEditor(target.GetComponentInChildren<ObjectiveTarget>());
        editorTarget.OnInspectorGUI();

        EditorGUILayout.Space();

        if (target.GetComponentInChildren<ObjectiveTarget>().FindObjectiveItemOfSameType() != null)
        {
            EditorGUILayout.LabelField("Objective Item", EditorStyles.boldLabel);
            EditorGUILayout.ObjectField("Objective Item Script", target.GetComponentInChildren<ObjectiveTarget>().FindObjectiveItemOfSameType(), typeof(ObjectiveItem), true);
            Editor editorItem = Editor.CreateEditor(target.GetComponentInChildren<ObjectiveTarget>().FindObjectiveItemOfSameType());
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
            Undo.DestroyObjectImmediate(target);
        }
    }
}
#endif