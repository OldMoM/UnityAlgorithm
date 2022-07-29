using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;

namespace LuaTestFramework
{
    public class TestTreeView : TreeView
    {
        private List<TreeViewItem> m_allItems = new List<TreeViewItem>();
        private TreeViewItem root = new TreeViewItem() { id = 0, depth = -1, displayName = "Root" };
        public List<TreeViewItem> AllItems => m_allItems;

        static Texture2D[] m_iconTexts =
        {
        (Texture2D)EditorGUIUtility.Load("idle.png"),
        (Texture2D)EditorGUIUtility.Load("pass.png"),
        (Texture2D)EditorGUIUtility.Load("fail.png"),
        };

        public TestTreeView(TreeViewState treeViewState)
            : base(treeViewState)
        {
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            SetupParentsAndChildrenFromDepths(root, m_allItems);
            return root;
        }

        public void SetDataList(List<TestTreeViewItem> items)
        {
            m_allItems.Clear();
            foreach (TestTreeViewItem item in items)
            {
                item.icon = m_iconTexts[(int)item.State];
                m_allItems.Add(item);
            }
            this.Reload();
        }

        public TreeViewItem FindItem(int id)
        {
            return m_allItems[id];
        }
    }
}
