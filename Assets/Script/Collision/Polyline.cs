using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Polyline : MonoBehaviour
{
    public Vector3[] vertex;
    public bool intersect;

    private void OnDrawGizmos()
    {
        if (vertex.Length > 1)
        {
            var draw_vertex = new Vector3[vertex.Length + 1];
            for (int i = 0; i < vertex.Length; i++)
            {
                draw_vertex[i] = vertex[i] + transform.position;
            }
            draw_vertex[vertex.Length] = vertex[0]+ transform.position;

            Handles.color = intersect ? Color.red : Color.white;
            Handles.DrawPolyLine(draw_vertex);
        }
    }

    public Vector3[] GetWorldVertex()
    {
        var vertex_world = new Vector3[vertex.Length];
        for (int i = 0; i < vertex.Length; i++)
        {
            vertex_world[i] = vertex[i] + transform.position;
        }
        return vertex_world;
    }
}
