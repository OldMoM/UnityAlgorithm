using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class MovementSystem :ComponentSystem
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
    }
}
