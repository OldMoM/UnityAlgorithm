using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单向链表节点
/// </summary>
public class ListNode<T>
{
    public T value;
    public ListNode<T> next;

    public ListNode(T val)
    {
        this.value = val;
    }
}
