using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringString : MonoBehaviour
{
    public Transform[] nodes;

    public List<int>[] node_connets;

    public SpringV2[] springs;

    public float elastity;

    public float damp;

    private Vector3[] position_matrix = new Vector3[7];
    private Vector3[] velocity_matrix = new Vector3[7];

    private Vector3[] temp_position_matrix = new Vector3[7];
    private Vector3[] temp_velocity_matrix = new Vector3[7];

    public float[,] spring_length_matrix;

    // Start is called before the first frame update
    void Start()
    {
        CreateSprings();

        node_connets = new List<int>[7];

        node_connets[0] = new List<int>() { 1 };
        node_connets[1] = new List<int>() { 0, 2 };
        node_connets[2] = new List<int>() { 1, 3 };
        node_connets[3] = new List<int>() { 2, 4 };
        node_connets[4] = new List<int>() { 3, 5 };
        node_connets[5] = new List<int>() { 4, 6 };
        node_connets[6] = new List<int>() { 5 };


        spring_length_matrix = new float[nodes.Length, nodes.Length];
        for (int i = 0; i < nodes.Length; i++)
        {
            position_matrix[i] = nodes[i].position;
            velocity_matrix[i] = Vector3.zero;


            for (int j = 0; j < node_connets[i].Count; j++)
            {
                var connect_node_index = node_connets[i][j];
                var distance = (nodes[i].position - nodes[connect_node_index].position).magnitude;
                spring_length_matrix[i, connect_node_index] = distance;
            }
        }
    }

    private void FixedUpdate()
    {
        //0号点作为固定点
        velocity_matrix[0] = (nodes[0].position - position_matrix[0]) / Time.fixedDeltaTime;
        position_matrix[0] = nodes[0].position;

        for (int i = 1; i < nodes.Length; i++)
        {
            position_matrix[i] = position_matrix[i] + Time.fixedDeltaTime * velocity_matrix[i];

            var spring_force = CalculatSpringForce(i);
            var damp_force = -damp * velocity_matrix[i];
            var gravity = Vector3.down * 9.81f;
            velocity_matrix[i] = velocity_matrix[i] + Time.fixedDeltaTime * (-(spring_force + damp_force) + gravity);
            //if (i==1)
            //{
            //    print((-(spring_force + damp_force) + gravity).y);
            //}
            nodes[i].position = position_matrix[i];
        }
    }

    private Vector3 CalculatSpringForce(int node_index)
    {
        var node_position = position_matrix[node_index];
        var force = Vector3.zero;
        for (int i = 0; i < node_connets[node_index].Count; i++)
        {
            var node_connected_index = node_connets[node_index][i];
            var node_connected = position_matrix[node_connected_index] ;
            var temp = node_position - node_connected;
            var lenght_start = spring_length_matrix[node_index, node_connected_index];
            var l_diff = temp.magnitude - lenght_start;
            force += l_diff * temp.normalized* elastity;
        }
        return force;
    }

    private void CreateSprings()
    {
        springs = new SpringV2[nodes.Length - 1];

        for (int i = 0; i < nodes.Length-1; i++)
        {
            //Gizmos.DrawLine()
            springs[i] = new SpringV2(nodes[i].position, nodes[i + 1].position, elastity);
        }
    }


    private void OnDrawGizmos()
    {
        //if (springs!= null && springs.Length > 0)
        //{
        //    for (int i = 0; i < springs.Length; i++)
        //    {
        //        Gizmos.DrawLine(springs[i].m_start, springs[i].m_end);
        //    }
        //}
    }
}
