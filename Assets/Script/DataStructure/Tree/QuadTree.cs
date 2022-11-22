using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree<T>:IEnumerable,IEnumerator
{
    private QuadTreeNode<T> root;
    private int capacity = 1;

    private List<QuadTreeNode<T>> record = new List<QuadTreeNode<T>>();
    private QuadTreeNode<T> record_header;

    public object Current => throw new System.NotImplementedException();

    public QuadTree(int boundary,T value)
    {
        root = new QuadTreeNode<T>(0, 0, value);
    }

    public void Insert(T value, int x, int y)
    {
        var header = root;
        InsertRecursion(header, x, y, value);
    }

    public void DSF()
    {
        var header = root;
        DSFRecursion(header);
    }

    private void DSFRecursion(QuadTreeNode<T> node)
    {
        if (node is null)
        {
            return;
        }
        DSFRecursion(node.children[0]);
        DSFRecursion(node.children[1]);
        DSFRecursion(node.children[2]);
        DSFRecursion(node.children[3]);
    }

    private int GetPointArea(QuadTreeNode<T> node,int x,int y)
    {
        return 0;
    }

    private void InsertRecursion(QuadTreeNode<T> header,int x, int y,T value)
    {
        
        var area = GetPointArea(header, x, y);
        if (header.children[area] == null)
        {
            header.children[area] = new QuadTreeNode<T>(x, y, value);
        }
        else
        {
            header = header.children[area];
            InsertRecursion(header, x, y, value);
        }
    }

    public IEnumerator GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    public bool MoveNext()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
