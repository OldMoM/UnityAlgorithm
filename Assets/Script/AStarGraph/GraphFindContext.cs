using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GraphFindContext :MonoBehaviour
{
    private Graph graph;

    public int start;
    public int destination;

    public Graph Graph => graph;


    private GraphView view;
    private Dictionary<int,Unit> visited = new Dictionary<int,Unit>();

    private void Start()
    {
        Init();

        view.onPushBtnClicked
            .Subscribe(x =>
            {
                var filter =
                graph.adjecents[start].ToObservable()
                .Where(p =>
                {
                    return !visited.ContainsKey(p);
                })
                .Select(p =>
                {
                    var startPos = graph.vertex[start];
                    var pPos = graph.vertex[p];
                    var desPos = graph.vertex[destination];
                    var _price = AStartSystem.GetTotalCost(startPos, pPos, desPos);
                    print(p + "点价格：" + _price);
                    view.DrawColor(p, Color.blue);

                    (int n, float price) t = (p, _price);

                    return t;
                })
                .Do(p =>
                {
                    print("--------------");
                });

                var min = AStartSystem.GetMinPrice(filter);
                start = min.pos;

                visited.Add(start,Unit.Default);
                view.DrawColor(start,Color.cyan);
            });
    }

    private void Init()
    {
        graph.vertex = new List<Vector3> ();
        graph.adjecents = new List<int[]> ();

        view = GetComponent<GraphView>();

        for (int i = 0; i < view.vertex.Length; i++)
        {
            AddVertex(view.vertex[i].position);
        }

        for (int i = 0; i < view.edge.Length; i++)
        {
            AddEdge(StringConvertSystem.ConvertToIntArray(view.edge[i]));
        }

        for (int i = 0; i < Graph.adjecents.Count; i++)
        {
            foreach (var item in Graph.adjecents[i])
            {
                view.DrawLine(graph, i, item);
            }
        }

        visited.Add(start,Unit.Default);

        view.DrawColor(start, Color.green);
        view.DrawColor(destination, Color.red);
    }

    public void AddVertex(Vector3 vertext)
    {
        graph.vertex.Add(vertext);
    }

    public void AddEdge(int[] adjecents)
    {
        graph.adjecents.Add(adjecents);
    }

    
}
