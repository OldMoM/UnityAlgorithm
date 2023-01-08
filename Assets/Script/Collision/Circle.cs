using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField]
    private float radius = 0.5f;

    public float Radius => radius;
    public Vector3 Center => transform.position;
}
