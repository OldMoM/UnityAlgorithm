using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor.IMGUI.Controls;
using System.Linq;

public class LuaTestFrameworkEditor : EditorWindow
{
    bool refresh;
    [SerializeField] TreeViewState m_TreeViewState;



    // The TreeView is not serializable it should be reconstructed from the tree data.
    TestTreeView m_TreeView;
    SearchField m_SearchField;
    [MenuItem("Tools/LuaTestFramework")]
    public static void OpenTestFrameworkWindow()
    {
        var window = EditorWindow.GetWindow(typeof(LuaTestFrameworkEditor));
        window.titleContent = new GUIContent("Lua Test Framework");

        var treeViewItem = new TreeViewItem();

    }

    private void OnEnable()
    {
        if (m_TreeViewState == null)
            m_TreeViewState = new TreeViewState();

        m_TreeView = new TestTreeView(m_TreeViewState);
        m_SearchField = new SearchField();
        m_SearchField.downOrUpArrowKeyPressed += m_TreeView.SetFocusAndEnsureSelectedItem;


    }

    private void OnGUI()
    {
        DrawSearchField();
        DrawToolBar();
        DrawTestTree();
    }

    private void DrawToolBar()
    {
        refresh = GUILayout.Button("Refresh");
        if (refresh)
        {
            //m_TreeView.SetDataList();
            ReadTestFromLua();
        }
    }

    private void ReadTestFromLua()
    {
        var fileList = new List<string>();
        var path = @"D:\UnityAlgorithm\Assets\LuaTests";
        var exist = Directory.Exists(path);
        if (exist)
        {
            var files = Directory.GetFiles(path);
 

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
    private void DrawSearchField()
    {
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUILayout.Space(100);
        GUILayout.FlexibleSpace();
        m_TreeView.searchString = m_SearchField.OnToolbarGUI(m_TreeView.searchString);
        GUILayout.EndHorizontal();
    }
    private void DrawTestTree()
    {
        Rect rect = GUILayoutUtility.GetRect(0, 100000, 0, 100000);
        m_TreeView.OnGUI(rect);
    }
}
