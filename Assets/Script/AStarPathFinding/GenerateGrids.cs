using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class GenerateGrids : MonoBehaviour
{
    public GameObject gridPrefab;
    public int gridSize = 5;
    public Transform rootNode;

    private List<GameObject> grid_Gos = new List<GameObject>();

    public void CreateGrids(List<(int num,Vector2Int pos,int type)> grids)
    {
        print(grids.Count);
        for (int i = 0; i < grids.Count; i++)
        {
            var grid_go = GameObject.Instantiate(gridPrefab);
            grid_go.SetActive(true);
            grid_go.transform.SetParent(rootNode);

            grid_Gos.Add(grid_go);

            if (grids[i].type == 1 )
            {
                grid_go.GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void SetStart(Vector2Int start)
    {
        var num = start.x * gridSize + start.y;
        grid_Gos[num].GetComponent<Image>().color = Color.green;
    }

    public void SetDetination(Vector2Int des)
    {
        var num = des.x * gridSize + des.y;
        grid_Gos[num].GetComponent <Image>().color = Color.red;

    }
    
    public void DrawSearchArea(Vector2Int pos)
    {
        var num = pos.x * gridSize + pos.y;
        grid_Gos[num].GetComponent<Image>().color = Color.blue;
    }

    
    public void DrawPrice(Vector2Int pos,int price)
    {
        var num = pos.x *gridSize+pos.y;
        grid_Gos[num].GetComponentInChildren<Text>().text = price.ToString();
    }

    public void DrawTarget(Vector2Int pos)
    {
        var num = pos.x * gridSize + pos.y;
        grid_Gos[num].GetComponent<Image>().color = Color.cyan;
    }
}
