using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyWindow : EditorWindow 
{
    string myString = "Hellow world";
    bool groupEnable = false;
    bool myBool = true;
    float myFloat = 1.23f;

    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        GetWindow<MyWindow>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field",myString);

        groupEnable = EditorGUILayout.BeginToggleGroup("Setting Optional",groupEnable);
        myBool = EditorGUILayout.Toggle("MyBool",myBool);
        myFloat = EditorGUILayout.Slider("My Slider",myFloat, 0, 100);
        EditorGUILayout.EndToggleGroup();

    }
}
