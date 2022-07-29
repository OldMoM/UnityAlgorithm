using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor.IMGUI.Controls;
using System.Linq;

namespace LuaTestFramework
{
    public class LuaTestFrameworkEditor : EditorWindow
    {
        bool m_refresh;
        bool m_runAll;
        bool m_runSelect;
        bool m_clearAll;
        [SerializeField] TreeViewState m_TreeViewState;

        string[] m_toolBarSetting = { "Refresh", "RunAll", "RunSelect" };
        int m_toolBarSelect;


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
            GUILayout.BeginHorizontal();
            DrawToolBar();
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            DrawTestTree();
            GUILayout.EndVertical();
        }

        private void DrawToolBar()
        {
            m_refresh = GUILayout.Button("Refresh",GUILayout.Width(100));
            m_runAll = GUILayout.Button("RunAll", GUILayout.Width(100));
            m_runSelect = GUILayout.Button("RunSelect", GUILayout.Width(100));
            m_clearAll = GUILayout.Button("Clear", GUILayout.Width(100));
            if (m_refresh)
            {
                ReadTestFromLua();
            }
            if (m_runSelect)
            {
                var selectIds = m_TreeView.GetSelection();
                foreach (var id in selectIds)
                {
                    var item = m_TreeView.FindItem(id);
                    LuaTestFramework.RunTest(item.displayName);
                }
            }
            if (m_runAll)
            {
                m_TreeView.AllItems.ForEach(item =>
                {
                    LuaTestFramework.RunTest(item.displayName);
                });
            }
        }
        private void ReadTestFromLua()
        {
            var fileList = new List<string>();
            var path = @"F:\Projects\test\Unity--\Assets\LuaTests";
            var exist = Directory.Exists(path);
            if (exist)
            {
                var files = Directory.GetFiles(path);

                Regex regex_getFileName = new Regex(@"(?<=LuaTests\\).*.lua");

                var files_filter = from file in files
                                   where file.EndsWith(".lua")
                                   select file;

                var _testTreeItem = new List<TestTreeViewItem>();
                var counter = 1;
                foreach (string file in files_filter)
                {
                    StreamReader reader = new StreamReader(file);
                    var content = reader.ReadToEnd();
                    //正则捕获
                    Regex regex = new Regex(@"(?<=function ).*?(?=\()");

                    //var method = regex.Match(content);
                    MatchCollection methods = regex.Matches(content);
                    //刷新深度0节点
                    var item_depth_0 = new TestTreeViewItem()
                    {
                        id = counter,
                        depth = 0,
                        displayName = file,
                        State = TestState.IDLE,
                    };
                    _testTreeItem.Add(item_depth_0);
                    counter++;

                    //刷新深度1节点
                    var count = methods.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var method = methods[i].Value;
                        //创建深度1节点
                        var item_depth_1 = new TestTreeViewItem()
                        {
                            id = counter,
                            depth = 1,
                            displayName = method,
                            State = TestState.IDLE,
                        };
                        _testTreeItem.Add(item_depth_1);
                        counter++;
                    }
                }
                m_TreeView.SetDataList(_testTreeItem);
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
}
