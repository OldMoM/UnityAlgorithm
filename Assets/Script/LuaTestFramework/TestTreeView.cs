using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;

public class TestTreeView : TreeView
{
	private List<TreeViewItem> m_allItems = new List<TreeViewItem>();
    private TreeViewItem root = new TreeViewItem() { id = 0, depth = -1, displayName = "Root" };

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

	public void SetDataList()
    {
        m_allItems = new List<TreeViewItem>
            {
                new TreeViewItem {id = 1, depth = 0, displayName = "Animals",icon = m_iconTexts[0]},
                new TreeViewItem {id = 2, depth = 1, displayName = "Mammals",icon = m_iconTexts[0]},
                new TreeViewItem {id = 3, depth = 2, displayName = "Tiger",icon = m_iconTexts[0]},
                new TreeViewItem {id = 4, depth = 2, displayName = "Elephant",icon = m_iconTexts[0]},
                new TreeViewItem {id = 5, depth = 2, displayName = "Okapi",icon = m_iconTexts[0]},
            };
        this.Reload();
    }
}
