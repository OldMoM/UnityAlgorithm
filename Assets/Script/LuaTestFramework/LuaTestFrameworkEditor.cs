using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class LuaTestFrameworkEditor : EditorWindow
{
    bool refresh;

    [MenuItem("Tools/LuaTestFramework")]
    public static void OpenTestFrameworkWindow()
    {
        var window = EditorWindow.GetWindow(typeof(LuaTestFrameworkEditor));
        window.titleContent = new GUIContent("Lua Test Framework");


    }

    private void OnGUI()
    {
        refresh = GUI.Button(new Rect(0, 0, 100, 40), new GUIContent("Refresh"));
        if (refresh)
        {
            var fileList = new List<string>();
            var path = @"D:\UnityAlgorithm\Assets\LuaTests";
            var exist = Directory.Exists(path);
            if (exist)
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    //fileList.Add(file);
                    StreamReader reader = new StreamReader(file);
                    var content = reader.ReadToEnd();
                    //正则捕获
                    Regex regex = new Regex(@"(?<=function ).*?(?=\()");
                   //var method = regex.Match(content);
                    MatchCollection methods = regex.Matches(content);
                    //Debug.Log(method);
                    var count = methods.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Debug.Log(methods[i].Value);
                    }
                }
            }
        }
    }
}
