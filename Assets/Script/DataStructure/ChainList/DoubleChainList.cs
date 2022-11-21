using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 双向链表
/// </summary>
public class DoubleChainList<T> 
{
    private DoubleChainedNode<T> start_header;
    private DoubleChainedNode<T> end_header;

    private int count = 0;
    private bool reversed = false;

    public DoubleChainList()
    {
        start_header = new DoubleChainedNode<T>();
        end_header = start_header;
    }

    public void Add(T value)
    {
        var new_node = new DoubleChainedNode<T>();

        //if (count == 0)
        //{
        //    start_header = new_node;
        //}

        new_node.value = value;
        end_header.next = new_node;
        new_node.prev = end_header;
        end_header = new_node;
        count++;
    }

    public void Insert(int index,T value)
    {

    }

    public void RemoveAt(int index)
    {

    }
    /// <summary>
    /// Forwards the search.
    /// </summary>
    /// <returns></returns>
    private T ForwardSearch(int index)
    { 
        DoubleChainedNode<T> temp_header = start_header;
        for (int i = 0; i < index; i++)
        {
            if (temp_header.next != null)
            {
                temp_header = temp_header.next;
            }
        }
        return temp_header.value;  
    }

    private T BackwardSearch(int index)
    {
        DoubleChainedNode<T> temp_header = end_header;
        for (int i = Count; i > index; i--)
        {
            if (temp_header.prev != null)
            {
                temp_header = temp_header.prev;
            }
        }
        return temp_header.value;
    }

    private bool ShouldForwardSearch(int index)
    {
        return index <= Count >> 1;
    }

    public T this[int index]
    {
        get
        {
            return ShouldForwardSearch(index) ? ForwardSearch(index) : BackwardSearch(index);
        }
    }
    public int Count => count;
    /// <summary>
    /// 反转链表只需要交换链表前后引用
    /// </summary>
    public void Reverse()
    {
        reversed = !reversed;
        var temp = start_header;
        start_header = end_header;
        start_header = temp;
    }
}
