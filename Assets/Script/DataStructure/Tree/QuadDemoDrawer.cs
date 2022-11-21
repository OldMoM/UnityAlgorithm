using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadDemoDrawer : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _origin;
    [SerializeField]
    private int boundary;
    [SerializeField]
    private Transform[] point_list;

    private QuadTree<int> quad;

    // Start is called before the first frame update
    void Start()
    {
        quad = new QuadTree<int>(boundary, 1);
    }

    private Vector3 Vecter2Int2Vecter3(Vector2Int vector2Int)
    {
        return new Vector3(vector2Int.x, vector2Int.y, 0);
    }

    private void DrawUILine(Vector2Int start,Vector2Int end)
    {
        var start_world = Camera.main.ScreenToWorldPoint(Vecter2Int2Vecter3(start + _origin)) + new Vector3(0, 0, 10);
        var end_world = Camera.main.ScreenToWorldPoint(Vecter2Int2Vecter3(end+_origin)) + new Vector3(0, 0, 10);
        Gizmos.DrawLine(start_world, end_world);
    }

    private void OnDrawGizmos()
    {
        //绘制边框
        var half_boundary = boundary >> 1;
        var left_top = new Vector2Int(-half_boundary, half_boundary);
        var right_top = new Vector2Int(half_boundary, half_boundary);
        var left_buttom= new Vector2Int(-half_boundary, -half_boundary);
        var right_buttom = new Vector2Int(half_boundary, -half_boundary);

        DrawUILine(left_top, right_top);
        DrawUILine(right_top, right_buttom);
        DrawUILine(right_buttom, left_buttom);
        DrawUILine(left_buttom, left_top);
    }

    public void DrawCrossLine(Vector2Int center,int half_edge)
    {
        var left = center + Vector2Int.left * half_edge;
        var right = center + Vector2Int.right * half_edge;
        var top = center + Vector2Int.up * half_edge;
        var down = center + Vector2Int.down * half_edge;
        DrawUILine(left, right);
        DrawUILine(top, down);
    }
}
