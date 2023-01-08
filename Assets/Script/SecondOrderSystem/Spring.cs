using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float omega = 2f;
    public float zeta = 0.5f;

    private Vector3 x;
    public Vector3 dx;

    public float T;

    private float damp;
    private float elastity;

    public Transform anchor;

    private float l;

    private Vector3 gravity;

   private
    void Start()
    {
        x = transform.position;
        dx = Vector3.zero;
        T = Time.fixedDeltaTime;

        damp = 2 * zeta * omega;
        elastity = omega * omega;

        l = (transform.position - anchor.position).sqrMagnitude;
        gravity = Vector3.down * 9.8f;
    }

    private void FixedUpdate()
    {
        transform.position = CalculatPos();
    }

    private Vector3 CalculatPos()
    {
        x = x + T * dx;

        var temp = (x - anchor.position);

        //假定弹簧不可压缩
        var l_diff = temp.sqrMagnitude - l;
        l_diff = Mathf.Clamp(l_diff, 0, 5 * l);
        var spring_force = l_diff * elastity * temp.normalized;

        dx = dx + T * (gravity - (damp * dx + spring_force));
        return x;
    }
}
