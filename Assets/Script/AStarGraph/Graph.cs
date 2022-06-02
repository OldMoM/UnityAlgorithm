using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Graph 
{
    /// <summary>
    /// 点表
    /// </summary>
    public List<Vector3> vertex;
    /// <summary>
    /// 邻接表
    /// </summary>
    public List<int[]> adjecents;
}
