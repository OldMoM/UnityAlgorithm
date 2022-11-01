using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 双向链表节点
/// </summary>
public class DoubleChainedNode<T> 
{
    public T value;
    public DoubleChainedNode<T> prev;
    public DoubleChainedNode<T> next;
}
