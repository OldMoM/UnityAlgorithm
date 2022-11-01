using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree:IEnumerable<int>
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
    private void Insert(int value,BinaryTreeNode<int> header)
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

    public IEnumerator<int> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}
