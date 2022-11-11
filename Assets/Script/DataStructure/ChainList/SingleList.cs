using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 自定义链表,空头链表
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingleList<T>
{
    private T m_value;

    private SingleList<T> next;

    private SingleListNode<T> start_header;

    private int count;
    public int Count =>count;

    public SingleList()
    {
        start_header = new SingleListNode<T>();
    }
    public SingleList<T> Add(T val)
    {
        // next = new SingleList<T>(val);
        // return next;
        return default;
    }

    public void RemoveAt(int index)
    {
        var before_node = this.GetNode(index - 1);
        var after_node = this.GetNode(index + 1);
        before_node.Next = after_node;
    }

    public void Insert(int index,T value)
    {
        // if (index == 0)
        // {
        //     throw new System.Exception("index cannot be 0");
        // }
        // else
        // {
        //     var before_node = GetNode(index - 1);
        //     var after_node = GetNode(index + 1);
        //     var new_node = new SingleList<T>(value);
        //     before_node.Next = new_node;
        //     new_node.Next = after_node;
        // }
    }

    public SingleList<T> Next
    {
        get => next;
        set { next = value; }
    }

    public T Value => this.m_value;

    public T this[int index]
    {
        get
        {
            var header = this.next;
            for (int i = 0; i < index; i++)
            {
                if (i == index)
                {
                    break;
                }
                if (i>0) header = header.next;
            }
            return header.Value;
        }
    }
    public SingleList<T> GetNode(int index)
    {
        var header = this.next;
        for (int i = 0; i < index; i++)
        {
            if (i == index)
            {
                break;
            }
            if (i > 0) header = header.next;
        }
        return header;
    }
}
