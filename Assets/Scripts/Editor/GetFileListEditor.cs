using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(GetFileList))]
[CanEditMultipleObjects]
public class GetFileListEditor : Editor
{
    SerializedProperty lookAtPoint;

    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("lookAtPoint");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GetFileList myScript = (GetFileList)target;

        if (GUILayout.Button("Populate File List"))
        {
            myScript.PopulateFileLists();
        }
    }
}
