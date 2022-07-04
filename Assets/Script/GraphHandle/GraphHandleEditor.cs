using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(GraphHandle))]
public class GraphHandleEditor : Editor
{
    GraphHandle handle;

    private void OnEnable()
    {
        handle = (GraphHandle)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("基本信息");

        if (GUILayout.Button("写入Json"))
        {
            var testArray = new int[4] { 1, 2, 3, 4 };
            var t = new TestNode();

            string json = EditorJsonUtility.ToJson(t);
            string filepath = Application.streamingAssetsPath + "/test.json";

            using(StreamWriter sw = new StreamWriter(filepath))
            {
                Debug.Log("写入Json");
                sw.WriteLine(json);
                sw.Close();
                sw.Dispose();
            }
        }

        EditorGUILayout.EndVertical();
    }
}
