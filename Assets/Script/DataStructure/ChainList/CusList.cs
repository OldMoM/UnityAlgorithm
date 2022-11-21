using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 自定义链表
/// </summary>
/// <typeparam name="T"></typeparam>
public class CusList<T>
{
    private ListNode<T> m_root;
    private ListNode<T> m_end_header;

    private int m_count;

    public CusList(T val)
    {
        m_root = new ListNode<T>(val);
        m_end_header = m_root;
        m_count++;
    }
    public void Add(T val)
    {
        m_end_header.next = new ListNode<T>(val);
        m_end_header = m_end_header.next;
        m_count++;
    }

    public void RemoveAt(int index)
    {
        var before_node = this.GetNode(index - 1);
        var after_node = this.GetNode(index + 1);
        before_node.next = after_node;
    }

    public void Insert(int index,T value)
    {
        if (index == 0)
        {
            throw new System.Exception("index cannot be 0");
        }
        else
        {
            var before_node = GetNode(index - 1);
            var after_node = GetNode(index + 1);
            var new_node = new ListNode<T>(value);
            before_node.next = new_node;
            new_node.next = after_node;
        }
    }
    public int Count
    {
        get => m_count;
    }

    public T this[int index]
    {
        get
        {
            var header = m_root;
            if (index == 0)
            {
                return header.value;
            }
            else
            {
                for (int i = 1; i <= index; i++)
                {
                    header = header.next;
                    if (i == index)
                    {
                        break;
                    }
                }

            }
            return header.value;
        }
    }
    private ListNode<T> GetNode(int index)
    {
        var header = m_root;
        if (index == 0)
        {
            return header;
        }
        else
        {
            for (int i = 1; i <= index; i++)
            {
                header = header.next;
                if (i == index)
                {
                    break;
                }
            }

        }
        return header;
    }
}
