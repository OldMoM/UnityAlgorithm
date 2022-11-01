using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree
{
    private BinaryTreeNode<int> root;
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

    public void Insert(int value)
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
        
        //DFSRecurrsion1(header);
        DFSRecurrsion2(header);

    }
    /// <summary>
    /// 这样子递归容易栈溢出
    /// </summary>
    /// <param name="header"></param>
    private void DFSRecurrsion1(BinaryTreeNode<int> header)
    {
        if (header == null)
        {
            return;
        }
        Debug.Log(header.value);
        DFSRecurrsion1(header.left);
        DFSRecurrsion1(header.right);
    }

    private void DFSRecurrsion2(BinaryTreeNode<int> header)
    {
        var visit = new Stack<BinaryTreeNode<int>>();
        while (header != null)
        {
            visit.Push(header);
            Debug.Log(header.value);
      
            if (header.left != null)
            {
                header = header.left;
            }
            else
            {
                var peek = visit.Pop();
                var last = visit.Peek();
                header = last.right; 
            }
        }
    }
}
