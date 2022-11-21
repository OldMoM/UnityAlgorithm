using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode<T>
{
    public QuadTreeNode<T>[] children = new QuadTreeNode<T>[4];
    private Vector2Int position;
    private T value;
    private bool is_leaf;
    public QuadTreeNode(int x,int y,T value)
    {
        this.value = value;
        position = new Vector2Int(x, y);
    }
    public bool IsLeaf => (children[0] is null && children[1] is null && children[2] is null && children[3] is null);
}
