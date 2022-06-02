using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

public class AStarEngine : MonoBehaviour
{
    public Button pushBtn;
    public int gridSize = 7;
    public GenerateGrids generateGrids;

    private List<(int num,Vector2Int pos,int type)> gridData = new List<(int num, Vector2Int pos, int type)>();

    private List<int> visited = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateGridData();
        GenerateBarrier(gridData,BarrieerTable.barrieers,gridSize);
        
        generateGrids.gridSize = gridSize;
        generateGrids.CreateGrids(gridData);
        generateGrids.SetStart(Vector2Int.zero);
        generateGrids.SetDetination(new Vector2Int(3, 3));

        Vector2Int start = Vector2Int.zero;
        Vector2Int destination = new Vector2Int(3, 3);

        pushBtn.OnClickAsObservable()
        .Subscribe(x=>{


            var obsevable =
            AStartSystem.GetAroundPos(start, gridData, gridSize, visited)
            .Select(y =>
            {
                generateGrids.DrawSearchArea(y);
                var _price = AStartSystem.GetTotalCost(start, y, destination);
                generateGrids.DrawPrice(y, _price);

                (Vector2Int, int) t = (y, _price);
                return t;
            });
            

            var min = AStartSystem.GetMinPrice(obsevable);
            start = min.pos;
            visited.Add(AStartSystem.PosToNum(min.pos, gridSize));

            generateGrids.DrawTarget(start);
        });
    }

    private AStarEngine GenerateGridData()
    {
        visited.Add(0);

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                var num = i*gridSize + j;
                var pos = new Vector2Int(i,j);
                (int num,Vector2Int pos,int type) data = (num,pos,0);
                gridData.Add(data);
            }
        }
        return this;
    }

    private void GenerateBarrier(List<(int num,Vector2Int pos,int type)> grid,in List<Vector2Int> barriers,int gridSize)
    {
       for (int i = 0; i < barriers.Count; i++)
       {
           var num = barriers[i].x * gridSize + barriers[i].y;
           var t = grid[num];
           t.type = 1;
           grid[num] = t;
       }
    }

}
