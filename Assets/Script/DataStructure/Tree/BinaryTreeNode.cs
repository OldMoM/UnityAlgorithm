using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeNode<T>
{
    public T value;
    public BinaryTreeNode<T> left;
    public BinaryTreeNode<T> right;
    public BinaryTreeNode<T> parent;

    public BinaryTreeNode(T value)
    {
        this.value = value;
    }
}
