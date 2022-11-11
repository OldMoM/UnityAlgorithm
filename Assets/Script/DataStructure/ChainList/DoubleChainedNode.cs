using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 双向链表节点
/// </summary>
public class DoubleChainedNode<T> 
{
    public T value;
    /// <summary>
    /// 前引用
    /// </summary>
    public DoubleChainedNode<T> prev;
    /// <summary>
    /// 后引用
    /// </summary>
    public DoubleChainedNode<T> next;
}
