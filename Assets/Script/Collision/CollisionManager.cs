using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
/// <summary>
/// SAT算法演示
/// </summary>
public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> childs = new List<Transform>();
    private int[,] collideMatrix;
    private int[] collideType;//0:多边形；1：圆


    private void Start()
    {
        foreach (Transform item in transform)
        {
            childs.Add(item);
        }
        collideMatrix = new int[childs.Count, childs.Count];
        collideType = new int[childs.Count];
        for (int i = 0; i < childs.Count; i++)
        {
            var circle = childs[i].GetComponent<Circle>();
            if (circle != null)
            {
                collideType[i] = 1;
            }
            else
            {
                collideType[i] = 0;
            }
       
        }
    }

    private Vector3 RotatePoint(Vector3 pivot,Vector3 rotate_point,float degree)
    {
        degree = Mathf.Deg2Rad * degree;
        var x_new = (rotate_point.x - pivot.x) * Mathf.Cos(degree) - (rotate_point.y - pivot.y) * Mathf.Sin(degree) + pivot.x;
        var y_new = (rotate_point.x - pivot.x) * Mathf.Sin(degree) + (rotate_point.y - pivot.y) * Mathf.Cos(degree) + pivot.y;
        return new Vector3(x_new, y_new, 0);
    }

    private Vector3[] GetVertex(Transform trans)
    {
        var size_half_x = trans.localScale.x/2;
        var size_half_y = trans.localScale.y/2;
        var center = trans.position;
        var rotate = trans.localRotation.eulerAngles.z;
        var vertexs = new Vector3[4];

        vertexs[0] = center + new Vector3(size_half_x, size_half_y, 0);
        vertexs[1] = center + new Vector3(-size_half_x, size_half_y, 0);
        vertexs[2] = center + new Vector3(-size_half_x, -size_half_y, 0);
        vertexs[3] = center + new Vector3(size_half_x, -size_half_y, 0);

        for (int i = 0; i < 4; i++)
        {
            vertexs[i] = RotatePoint(center, vertexs[i], rotate);
        }

        return vertexs;
    }

    private List<Vector3> GetProjectEdges(Vector3[] vertexs_1, Vector3[] vertexs_2)
    {
        var project_edges = new List<Vector3>();
        for (int i = 0; i < vertexs_1.Length; i++)
        {
            var start_point = vertexs_1[i];
            var end_point_index = i + 1;
            if (i == vertexs_1.Length - 1 )
            {
                end_point_index = 0;
            }

            var project = (vertexs_1[end_point_index] - start_point).normalized;
            project_edges.Add(project);
        }

        for (int i = 0; i < vertexs_2.Length; i++)
        {
            var start_point = vertexs_2[i];
            var end_point_index = i + 1;
            if (i == vertexs_2.Length - 1)
            {
                end_point_index = 0;
            }

            var project = (vertexs_2[end_point_index] - start_point).normalized;
            project_edges.Add(project);
        }

        return project_edges;
    }

    /// <summary>
    /// 检查两个投影范围是否重叠
    /// </summary>
    /// <param name="project_1"></param>
    /// <param name="project_2"></param>
    /// <returns></returns>
    private bool IsOverlap(float[] project_1,float[] project_2)
    {
        var length_1_half = Mathf.Abs(project_1[1] - project_1[0])/2;
        var center_1 = (project_1[0] + project_1[1])/2;
        var length_2_half = Mathf.Abs(project_2[1] - project_2[0])/2;
        var center_2 = (project_2[0] + project_2[1])/2;
        return (Mathf.Abs(center_2 - center_1) - (length_1_half + length_2_half)) <= 0;
    }

    /// <summary>
    /// 计算投影范围
    /// </summary>
    /// <param name="vertexs"></param>
    /// <param name="project"></param>
    /// <returns></returns>
    private float[] GetVertexsProjectrion(Vector3[] vertexs,Vector3 project)
    {
        var pos = new float[vertexs.Length];
        for (int i = 0; i < vertexs.Length; i++)
        {
            pos[i] = Vector3.Dot(vertexs[i], project);
        }
        var max = pos.Max();
        var min = pos.Min();
        return new float[2] { min, max };
    }

    private float[] GetCircleProjection(Vector3 center,float radius,Vector3 project)
    {
        var project_center = Vector3.Dot(center, project);
        var start = (project_center-radius) ;
        var end = (project_center + radius) ;

        return new float[2] { start, end };
    }

    private bool IsCollision(Vector3[] vertexs_1,Vector3[] vertexs_2)
    {
        var project_edges = GetProjectEdges(vertexs_1, vertexs_2);
        var overlaped = true;
        for (int i = 0; i < project_edges.Count; i++)
        {
            var project_1 = GetVertexsProjectrion(vertexs_1, project_edges[i]);
            var project_2 = GetVertexsProjectrion(vertexs_2, project_edges[i]);
            var is_overlaped = IsOverlap(project_1, project_2);
            overlaped = overlaped && is_overlaped;
        }

        return overlaped;
    }

    private bool PointAtLineRight(Vector3 point,Vector3 line_point_1,Vector3 line_point_2)
    {
        var A = line_point_2.y - line_point_1.y;
        var B = line_point_2.x - line_point_1.x;
        var C = line_point_2.x * line_point_1.y - line_point_1.x * line_point_2.y;
        var D = A * point.x + B * point.y + C;
        return D > 0;
    }

    private bool PolygonCollideWithCircle(Vector3[] vertexs_1,Circle circle)
    {
        var collide = true;
        var project_edge = new Vector3[vertexs_1.Length + 1];

        var minDisToCenter_index = 0;
        float dis_to_center = 999999999;
        //多边形投影轴
        for (int i = 0; i < vertexs_1.Length; i++)
        {
            var start_index = i;
            var end_index = i + 1;
            if (end_index >=vertexs_1.Length)
            {
                end_index = 0;
            }
            project_edge[i] = vertexs_1[end_index] - vertexs_1[start_index];

            //找到离圆心最近点
            var dis = (circle.Center - vertexs_1[i]).magnitude;
            if (dis < dis_to_center)
            {
                dis_to_center = dis;
                minDisToCenter_index = i;
            }
        }
        //圆的代表投影轴
        project_edge[vertexs_1.Length] = (circle.Center - vertexs_1[minDisToCenter_index]).normalized;

        for (int i = 0; i < project_edge.Length; i++)
        {
            var polygon_projection = GetVertexsProjectrion(vertexs_1, project_edge[i]);
            var circle_projection = GetCircleProjection(circle.Center, circle.Radius, project_edge[i]);
            var is_overlap = IsOverlap(polygon_projection, circle_projection);
            collide = collide && is_overlap;
        }

        return collide;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < childs.Count; i++)
        {
            for (int j = i+1; j < childs.Count; j++)
            {
                //检查碰撞类型
                var collide_type_1 = collideType[i];
                var collide_type_2 = collideType[j];

                var is_collided = false;

                if (collide_type_1 == 0 && collide_type_2 == 0)
                {
                    var vertex_1 = GetVertex(childs[i]);
                    var vertex_2 = GetVertex(childs[j]);
                    is_collided = IsCollision(vertex_1, vertex_2);
  
                }

                if (collide_type_1 == 0 && collide_type_2 == 1)
                {
                    var vertex_1 = GetVertex(childs[i]);
                    var circle = childs[j].GetComponent<Circle>();
                    is_collided = PolygonCollideWithCircle(vertex_1, circle);
                }

                if (collide_type_1 == 1 && collide_type_2 == 0)
                {
                    var vertex = GetVertex(childs[j]);
                    var circle = childs[i].GetComponent<Circle>();
                    is_collided = PolygonCollideWithCircle(vertex, circle);
                }

                collideMatrix[i, j] = is_collided ? 1 : 0;
                collideMatrix[j, i] = is_collided ? 1 : 0;
            }
        }

        //标记Sprite
        for (int i = 0; i < childs.Count; i++)
        {
            var isCollide = 0;
            for (int j = 0; j < childs.Count; j++)
            {
                isCollide = isCollide | collideMatrix[i, j];
            }
            if (isCollide > 0)
            {
                SetSpriteColor(childs[i], Color.red);
            }
            else
            {
                SetSpriteColor(childs[i], Color.white);
            }
        }
    }
    private void SetSpriteColor(Transform trans,Color color)
    {
        var spriteRender = trans.GetComponent<SpriteRenderer>();
        spriteRender.color = color;
    }

    private void OnDrawGizmos()
    {
        if (childs.Count > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < childs.Count; i++)
            {
                var child = childs[i];
                var vertexs = GetVertex(child);
                for (int k = 0; k < vertexs.Length; k++)
                {
                    Gizmos.DrawWireSphere(vertexs[k], 0.03f);
                }
            }
        }
    }
}
