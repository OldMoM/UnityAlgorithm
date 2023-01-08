using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderDynamics : MonoBehaviour
{
    [Range(1,100)]
    public float f;
    [Range(0,5)]
    public float zeta;
    [Range(-5,5)]
    public float r;


    private float k1, k2,k3;

    public Vector3 xp,xd;
    public Vector3 y, yd;
   
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        xp = transform.position;
        y = transform.position;
        yd = Vector3.zero;
        xd = Vector3.zero;

        k1 = zeta / (Mathf.PI * f);
        k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * zeta / (2 * Mathf.PI * f);
    }

    private void FixedUpdate()
    {
        var position = CalculatePosition(target.position);
        transform.position = position;
    }

    public float K1
    {
        get
        {
            return zeta / (Mathf.PI * f);
        }
    }

    public float K2=> 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
    public float K3=> r * zeta / (2 * Mathf.PI * f);

    private Vector3 CalculatePosition(Vector3 x)
    {
        xd = (x - xp) / Time.fixedDeltaTime;
        xp = x;
        y = y + Time.fixedDeltaTime * yd;

        var k2_stable = Mathf.Max(K2, Time.fixedDeltaTime * Time.fixedDeltaTime / 4 + k1 / 2);

        yd = yd + 1.2f*Time.fixedDeltaTime * (x + K3 * xd - y - K1 * yd) / k2_stable;
       
        return y;
    }
}
