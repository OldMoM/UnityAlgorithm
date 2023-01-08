using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using UniRx;

/// <summary>
/// GJK算法
/// </summary>
public class GJKCollision : MonoBehaviour
{
    Polyline[] polylines;

    bool intersect;
    //private List<Vector3> simplex = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        polylines = FindObjectsOfType<Polyline>();
        //var contains= TriangleContainsOrigin(Vector3.up, new Vector3(1, -1), new Vector3(-1, -1), new Vector3(2,2));
        //print(contains);

        var intersect = GJKInterSection(polylines[0], polylines[1]);
        print(intersect);

        //var vertical_dir = GetVerticalDirPointToOrigin(new Vector3(-3, -1.7f), new Vector3(1, 1.7f));
        //print(vertical_dir);
    }

    private void FixedUpdate()
    {
        intersect = GJKInterSection(polylines[0], polylines[1]);
        polylines[0].intersect = intersect;
        polylines[1].intersect = intersect;
    }

    private bool GJKInterSection(Polyline poly_1,Polyline poly_2)
    {
        var simplex = new List<Vector3>();
        var start_dir = Vector3.up;
        var support = Support(poly_1, poly_2, start_dir);
        simplex.Add(support);
        var intersect = GJKIterate(poly_1, poly_2, -start_dir, simplex);
        return intersect;
    }

    /// <summary>
    /// Polyline在给定方向上最远点
    /// </summary>
    /// <param name="poly">The poly.</param>
    /// <param name="dir">The dir.</param>
    /// <returns></returns>
    private Vector3 GetFurthestPointAtDir(Polyline poly,Vector3 dir)
    {
        var vertex_world = poly.GetWorldVertex();
        var dis_list = new float[vertex_world.Length];
        for (int i = 0; i < dis_list.Length; i++)
        {
            dis_list[i] = Vector3.Dot(vertex_world[i], dir.normalized);
        }
        var max = dis_list.Max();
        var max_index = Array.IndexOf(dis_list, max);
        return vertex_world[max_index];
    }

    private Vector3 Support(Polyline poly_1,Polyline poly_2,Vector3 dir)
    {
        var furthest_point_1 = GetFurthestPointAtDir(poly_1, dir);
        var furthest_point_2 = GetFurthestPointAtDir(poly_2, -dir);
        return furthest_point_1 - furthest_point_2;
    }

    //FIXME
    /// <summary>
    /// 三角形是否包含原点,三点必须逆时针
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <returns></returns>
    private bool TriangleContainsOrigin(Vector3 pA, Vector3 pB, Vector3 pC,Vector3 P)
    {
        //var PA = pA - P;
        //var PB = pB - P;
        //var PC = pC - P;
        //var t1 = Vector3.Cross(PA, PB);
        //var t2 = Vector3.Cross(PB, PC);
        //var t3 = Vector3.Cross(PC, PA);

        //return t1.z > 0 && t2.z > 0 && t3.z > 0;

        var AB = pB - pA;
        var AC = pC - pA;
        var AP = P - pA;

        var AC_dot_AB = Vector3.Dot(AC, AB);
        var AP_dot_AC = Vector3.Dot(AP, AC);
        var AP_dot_AB = Vector3.Dot(AP, AB);

        var u = (AB.sqrMagnitude * AP_dot_AC - AC_dot_AB * AP_dot_AB) /
                (AC.sqrMagnitude * AB.sqrMagnitude - AC_dot_AB * AC_dot_AB);

        var v = (AC.sqrMagnitude * AP_dot_AB - AC_dot_AB * AP_dot_AC) /
                (AC.sqrMagnitude * AB.sqrMagnitude - AC_dot_AB * AC_dot_AB);

        var condition_1 = u >= 0 && v <= 1;
        var condition_2 = v >= 0 && v <= 1;
        var condition_3 = u + v <= 1;

        return condition_1 && condition_2 && condition_3;
    }

    /// <summary>
    /// 计算AB直线朝向原点的垂线方向
    /// </summary>
    /// <param name="pA"></param>
    /// <param name="pB"></param>
    /// <returns></returns>
    private Vector3 GetVerticalDirPointToOrigin(Vector3 pA,Vector3 pB)
    {
        var dir = pB - pA;
        var vertical_dir_1 = new Vector3(-dir.y, dir.x, 0);
        var vertical_dir_2 = -vertical_dir_1;

        var pC = pA + vertical_dir_1;
        var pD = pA + vertical_dir_2;

        if (pC.magnitude < pD.magnitude)
        {
            return vertical_dir_1.normalized;
        }

        return vertical_dir_2.normalized;
    }

    private float GetDistanceBetweenLineAndPoint(Vector3 line_point_1,Vector3 line_point_2,Vector3 C)
    {
        var area = Vector3.Cross(line_point_1, line_point_2).magnitude;
        var line_vec = (line_point_2 - line_point_1);
        return area / line_vec.magnitude;
    }

    private Vector3 GetNewIterateDir(Vector3 pA, Vector3 pB,Vector3 pC,out List<Vector3> simplex)
    {
        simplex = new List<Vector3>();
        var dis_list = new float[3];
        //计算边到原点的距离
        dis_list[0] = GetDistanceBetweenLineAndPoint(pA, pB, Vector3.zero);
        dis_list[1] = GetDistanceBetweenLineAndPoint(pB, pC, Vector3.zero);
        dis_list[2] = GetDistanceBetweenLineAndPoint(pC, pA, Vector3.zero);
        //找到最近的边

        var min = dis_list.Min();
        var min_index = Array.IndexOf(dis_list, min);
        if (min_index == 0)
        {
            simplex.Add(pA);
            simplex.Add(pB);
            return GetVerticalDirPointToOrigin(pA, pB);
        }
        else if (min_index == 1)
        {
            simplex.Add(pB);
            simplex.Add(pC);
            return GetVerticalDirPointToOrigin(pB, pC);
        }
        else
        {
            simplex.Add(pC);
            simplex.Add(pA);
            return GetVerticalDirPointToOrigin(pC, pA);
        }
    }

    private bool IsParadox(Vector3 old_iterate_dir, Vector3 new_iterate_dir)
    {
        var diff = new_iterate_dir - old_iterate_dir;
        return diff.magnitude <= 0.01f;
    }

    private bool GJKIterate(Polyline poly_1, Polyline poly_2,Vector3 init_dir,List<Vector3> simplex)
    {
        var support = Support(poly_1, poly_2, init_dir);
        simplex.Add(support);
        //simplex中两个点，朝向原点垂线作为下次迭代方向

        //simplex中三个点
        //检查单纯形是否包含原点
        //检查是否跨越圆点，如果新Support无法跨越圆点，那么无法找到包含原点的单纯形
        //确立迭代方向

        if (simplex.Count == 2)
        {
            var vertical_dir = GetVerticalDirPointToOrigin(simplex[0], simplex[1]);
            return GJKIterate(poly_1, poly_2, vertical_dir, simplex);
        }

        if (simplex.Count == 3)
        {
            var contains_origin = TriangleContainsOrigin(simplex[0], simplex[1], simplex[2],Vector3.zero);
            if (contains_origin)
            {
                //intersect = true;
                return true;
            }

            //无法跨越圆点
            if (Vector3.Dot(support, init_dir) < 0)
            {
                //intersect = false;
                return false;
            }

            List<Vector3> new_simplex;
            var vertical_dir = GetNewIterateDir(simplex[0], simplex[1], simplex[2],out new_simplex);

            //检查奇异情况
            if (IsParadox(init_dir,vertical_dir))
            {
                return false;
            }

            return GJKIterate(poly_1, poly_2, vertical_dir, new_simplex);
        }

        return false;
    }
}
