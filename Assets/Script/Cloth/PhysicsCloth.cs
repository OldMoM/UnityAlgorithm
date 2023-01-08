using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCloth : MonoBehaviour
{
    Mesh mesh;
    int[] edge_list;
    float[] spring_origin_length_list;
    Vector3[] velocity_list;

    readonly float Damp = 0.99f;
    readonly float Spring_k = 8000;//弹簧拉伸系数
    readonly Vector3 Gravity = new Vector3(0, -9.8f, 0);
    // Start is called before the first frame update
    void Start()
    {
        ResizeMesh(21);
        CreateEdgeList(mesh.triangles);
        InitArgs();
    }
    private void InitArgs()
    {
        //速度矩阵
        velocity_list = new Vector3[mesh.vertexCount];
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            velocity_list[i] = Vector3.zero;
        }
        //记录弹簧原长
        spring_origin_length_list = new float[edge_list.Length / 2];
        for (int i = 0; i < spring_origin_length_list.Length; i++)
        {
            var vertex_start_index = edge_list[i * 2];
            var vertex_end_index = edge_list[i * 2 + 1];
            spring_origin_length_list[i] = (mesh.vertices[vertex_start_index] - mesh.vertices[vertex_end_index]).magnitude;
        }
    }
    private void ResizeMesh(int n)
    {
        mesh = GetComponent<MeshFilter>().mesh;
        //Resize mesh
        Vector3[] X = new Vector3[n * n];
        Vector2[] UV = new Vector2[n * n];
        int[] triangles = new int[(n - 1) * (n - 1) * 6];
        for (int j = 0; j < n; j++)
        {
            for (int i = 0; i < n; i++)
            {
                X[j * n + i] = new Vector3(5 - 10.0f * i / (n - 1), 0, 5 - 10.0f * j / (n - 1));
                UV[j * n + i] = new Vector3(i / (n - 1.0f), j / (n - 1.0f));
            }
        }

        int t = 0;
        for (int j = 0; j < n - 1; j++)
        {
            for (int i = 0; i < n - 1; i++)
            {
                triangles[t * 6 + 0] = j * n + i;
                triangles[t * 6 + 1] = j * n + i + 1;
                triangles[t * 6 + 2] = (j + 1) * n + i + 1;
                triangles[t * 6 + 3] = j * n + i;
                triangles[t * 6 + 4] = (j + 1) * n + i + 1;
                triangles[t * 6 + 5] = (j + 1) * n + i;
                t++;
            }
        }

        mesh.vertices = X;
        mesh.triangles = triangles;
        mesh.uv = UV;
        mesh.RecalculateNormals();
    }
    private void CreateEdgeList(int[] triangles)
    {
        //Construct the original E
        int[] _E = new int[triangles.Length * 2];
        for (int i = 0; i < triangles.Length; i += 3)
        {
            _E[i * 2 + 0] = triangles[i + 0];
            _E[i * 2 + 1] = triangles[i + 1];
            _E[i * 2 + 2] = triangles[i + 1];
            _E[i * 2 + 3] = triangles[i + 2];
            _E[i * 2 + 4] = triangles[i + 2];
            _E[i * 2 + 5] = triangles[i + 0];
        }

        //Reorder the original edge list
        for (int i = 0; i < _E.Length; i += 2)
            if (_E[i] > _E[i + 1])
                Swap(ref _E[i], ref _E[i + 1]);
    }
    void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
    void CalculateVelocity() { 
    }

    /// <summary>
    /// Calculates the gradient.
    /// </summary>
    /// <param name="X">坐标状态矩阵</param>
    /// <param name="X_hat">The x hat.</param>
    /// <param name="t">时间步长</param>
    void CalculateGradient(Vector3[] X, Vector3[] X_hat, float t)
    {
        //计算重力

        //计算惯性力

        //计算弹簧力
        for (int i = 0; i < edge_list.Length; i++)
        {
            var vertex_0 = edge_list[i * 2];
            var vertex_1 = edge_list[i * 2 + 1];

            var diff_vertex = vertex_0 - vertex_1;
            var sprint_force = Spring_k * (diff_vertex);
        }
    }

    private void FixedUpdate()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] X = mesh.vertices;
        Vector3[] last_X = new Vector3[X.Length];
        Vector3[] X_hat = new Vector3[X.Length];
        Vector3[] G = new Vector3[X.Length];

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                Gizmos.DrawWireSphere(mesh.vertices[i], .05f);
            }
        }
    }
}
