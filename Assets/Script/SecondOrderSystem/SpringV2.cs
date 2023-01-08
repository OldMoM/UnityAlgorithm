using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpringV2 
{
    private float elastic_modulus;
    public Vector3 m_start;
    public Vector3 m_end;
    private float length;

    public SpringV2(Vector3 start,Vector3 end,float em)
    {
        m_start = start;
        m_end = end;
        elastic_modulus = em;
        length = (m_end - m_start).sqrMagnitude;
    }
}
