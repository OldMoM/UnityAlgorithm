using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree
{
    protected BinaryTreeNode<int> root;
    public BinaryTreeNode<int> Root => root;

    public BinaryTree(int value)
    {
        root = new BinaryTreeNode<int>(value);
    }
    public BinaryTree(int[] array)
    {
        root = new BinaryTreeNode<int>(array[1]);
        for (int i = 1; i < array.Length; i++)
        {
            this.Insert(array[i]);
        }
    }

    public BinaryTree() { }

    /// <summary>
    /// 一般插入
    /// </summary>
    /// <param name="value"></param>
    public virtual void Insert(int value)
    {
        var header = root;
        Insert(value, header);
    }

    private void Insert(int value, BinaryTreeNode<int> header)
    {
        if (value <= header.value)
        {
            if (header.left == null)
            {
                header.left = new BinaryTreeNode<int>(value);
            }
            else
            {
                Insert(value, header.left);
            }
        }
        else
        {
            if (header.right == null)
            {
                header.right = new BinaryTreeNode<int>(value);
            }
            else
            {
                Insert(value, header.right);
            }
        }
    }
    public BinaryTreeNode<int> GetNode(int number)
    {
        return default;
    }

    private BinaryTreeNode<int> ForwardRight(BinaryTreeNode<int> header)
    {
        if (header.right != null)
        {
            header = header.right;
            return ForwardRight(header);
        }
        else
        {
            return header;
        }
    }

    public int Max()
    {
        var header = root;
        return ForwardRight(header).value;
    }

    public void DFS()
    {
        var header = root;
        DFSRecurrsion(header);
    }
    public int Height => FindHeight(this.root);

    /// <summary>
    /// 这样子递归容易栈溢出
    /// </summary>
    /// <param name="header"></param>
    private void DFSRecurrsion(BinaryTreeNode<int> header)
    {
        if (header == null)
        {
            return;
        }
        Debug.Log(header.value);
        DFSRecurrsion(header.left);
        DFSRecurrsion(header.right);
    }

    protected int FindHeight(BinaryTreeNode<int> root)
    {
        if (root == null)
        {
            return -1;
        }
        var left_height = FindHeight(root.left);
        var right_height = FindHeight(root.right);
        return Mathf.Max(left_height, right_height) + 1;
    }
}
