using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class GraphView : MonoBehaviour
{
    public Transform vertexNode;
    public Transform lineNode;
    public GameObject linePre;
    public Button pushBtn;

    public Transform[] vertex;
    public string[] edge;
    private Dictionary<(int s, int d), GameObject> edgeGos = new Dictionary<(int s, int d), GameObject>();
    private GraphFindContext context;

    public IObservable<Unit> onPushBtnClicked => OnPushBtnClicked();

    void Start()
    {
        //context = GetComponent<GraphFindContext>();
        //context.Init();
        //for (int i = 0; i < vertex.Length; i++)
        //{
        //    context.AddVertex(vertex[i].position);
        //}

        //for (int i = 0;i < edge.Length; i++)
        //{
        //    context.AddEdge(StringConvertSystem.ConvertToIntArray(edge[i]));
        //}

        //for (int i = 0; i < context.Graph.adjecents.Count; i++)
        //{
        //    foreach (var item in context.Graph.adjecents[i])
        //    {
        //        DrawLine(context.Graph, i, item);
        //    }
        //}
    }

    public void DrawLine(Graph graph,int s,int d)
    { 
        var hasEdge = edgeGos.ContainsKey((s, d));
        if (!hasEdge)
        {
            var _edge = GameObject.Instantiate(linePre,lineNode);
            var start_screen = graph.vertex[s];
            var des_sceen    = graph.vertex[d];
            var diff = des_sceen - start_screen;
            var dis = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y);
            var dir = diff.normalized;
            var center = (des_sceen + start_screen) / 2;

            var rectTransform = _edge.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(dis, 4);
            rectTransform.localPosition = center;
            rectTransform.right = dir;
        }
    }

    public void DrawColor(int n,Color color)
    {
        vertex[n].GetComponentInChildren<Image>().color = color;
    }

    public IObservable<Unit> OnPushBtnClicked()
    { 
        return pushBtn.OnClickAsObservable();
    }
}
