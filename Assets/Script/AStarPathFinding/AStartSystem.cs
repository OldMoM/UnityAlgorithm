using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public static class AStartSystem
{
    public static void StartRecursive()
    {

    }

    /// <summary>
    /// Gets the mahanttan distance.
    /// </summary>
    /// <param name="start">The start.</param>
    /// <param name="destination">The destination.</param>
    /// <returns></returns>
    public static int GetMahanttanDistance(Vector2Int start,Vector2Int destination){
        return Math.Abs(destination.x - start.x)
             + Math.Abs(destination.y - start.y);
    }

    /// <summary>
    /// Gets the mahanttan distance.
    /// </summary>
    /// <param name="start">The start.</param>
    /// <param name="destination">The destination.</param>
    /// <returns></returns>
    public static float GetMahanttanDistance(Vector3 start, Vector3 destination)
    {
        return Math.Abs(destination.x - start.x)
             + Math.Abs(destination.y - start.y);
    }


    /// <summary>
    /// 划定搜索范围
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="grid"></param>
    /// <param name="size"></param>
    /// <param name="visited"></param>
    /// <returns></returns>
    public static IObservable<Vector2Int> GetAroundPos(Vector2Int pos, List<(int num, Vector2Int pos, int type)> grid,int size,List<int> visited,int round = 1)
    {
        var aroundPos = new List<Vector2Int>();
        var start1 = pos + new Vector2Int(1, 1);
        var start2 = pos + new Vector2Int(-1, 1);
        var start3 = pos + new Vector2Int(-1, -1);
        var start4 = pos + new Vector2Int(1, -1);

        //aroundPos.Add(pos + Vector2Int.right);
        //aroundPos.Add(pos + new Vector2Int(1,1));
        //aroundPos.Add(pos + new Vector2Int(0, 1));
        //aroundPos.Add(pos + new Vector2Int(-1, 1));
        //aroundPos.Add(pos + new Vector2Int(-1, 0));
        //aroundPos.Add(pos + new Vector2Int(-1, -1));
        //aroundPos.Add(pos + new Vector2Int(0, -1));
        //aroundPos.Add(pos + new Vector2Int(1, -1));

        for (int i = 1; i < round+1; i++)
        {
            var t = start1 + i * Vector2Int.left;
            aroundPos.Add(t);
        }
        for (int i = 1; i < round + 1; i++)
        {
            var t = start2 + i * Vector2Int.down;
            aroundPos.Add(t);
        }
        for (int i = 1; i < round + 1; i++)
        {
            var t = start3 + i * Vector2Int.right;
            aroundPos.Add(t);
        }
        for (int i = 1; i < round + 1; i++)
        {
            var t = start4 + i * Vector2Int.up;
            aroundPos.Add(t);
        }


        return aroundPos
            .ToObservable()
            .Where(p =>
            {
                //确保点在网格内
                var xLimit = p.x >= 0 && p.x <= size;
                var yLimit = p.y >= 0 && p.y <= size;
                return xLimit && yLimit;
            })
            .Where(p =>
            {
                //检查周围点是否为障碍
                var num = p.x * size + p.y;
                var isBarrier = grid[num].type == 1;
                //是否已经访问
                var visitedPos = visited.Find(x => { return x == num; }) > 0;
                return !isBarrier && !visitedPos;
            });
    }

    //评估start点到target的代价
    public static int GetTotalCost(Vector2Int start,Vector2Int target,Vector2Int destination)
    {
        //return GetMahanttanDistance(start,target)+ GetMahanttanDistance(target,destination);
        return GetMahanttanDistance(target, destination);
    }

    /// <summary>
    /// Gets the total cost.
    /// </summary>
    /// <param name="start">The start.</param>
    /// <param name="target">The target.</param>
    /// <param name="destination">The destination.</param>
    /// <returns></returns>
    public static float GetTotalCost(Vector3 start, Vector3 target, Vector3 destination)
    {
        //return GetMahanttanDistance(start,target)+ GetMahanttanDistance(target,destination);
        return GetMahanttanDistance(target,destination);
    }

    public static (Vector2Int pos,int price) GetMinPrice(IObservable<(Vector2Int pos, int price)> observable)
    {
        (Vector2Int pos, int price) min = default;

        observable.First().Subscribe(x=> min = x);
        observable.Skip(1).Subscribe(x =>
        {
            if (x.price < min.price)
            {
                min = x;
            }
        });

        return min;
    }

    public static (int pos, float price) GetMinPrice(IObservable<(int pos, float price)> observable)
    {
        (int pos, float price) min = default;

        observable.First().Subscribe(x => min = x);
        observable.Skip(1).Subscribe(x =>
        {
            if (x.price < min.price)
            {
                min = x;
            }
        });

        return min;
    }

    public static int PosToNum(Vector2Int pos,int size)
    {
        return pos.x * size + pos.y;
    }
}
