﻿/// <summary>
/// 双向链表
/// </summary>
public class DoubleChainList<T> 
{   

    /// <summary>
    /// 头指针
    /// </summary>
    private DoubleChainedNode<T> start_header;
    /// <summary>
    /// 尾指针
    /// </summary>
    private DoubleChainedNode<T> end_header;

    private int count = 0;

    public DoubleChainList()
    {
        start_header = new DoubleChainedNode<T>();
        end_header = start_header;
    }

    public void Add(T value)
    {
        var new_node = new DoubleChainedNode<T>();
        new_node.value = value;
        end_header.next = new_node;
        new_node.prev = end_header;
        end_header = new_node;
        count++;
    }

    public void Insert(int index,T value)
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
        return index <= Count<<1;
    }

    public T this[int index]
    {
        get
        {
            return ShouldForwardSearch(index) ? ForwardSearch(index) : BackwardSearch(index);
        }
    }
    public int Count => count;
}
