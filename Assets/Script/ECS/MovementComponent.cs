using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using Unity.Entities;
using Unity.Mathematics;

public struct Position : IComponentData
{
    public float3 Value;
}

public struct Velocity : IComponentData
{
    public float3 Value;
}
