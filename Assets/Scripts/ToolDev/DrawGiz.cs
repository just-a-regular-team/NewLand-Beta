using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawGiz
{
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.02f)
    {
    GameObject myLine = GetOrInstance_GObjAsGiz("drawline",start,Quaternion.identity); 
     
    LineRenderer lr = myLine.GetComponent<LineRenderer>();
    //lr.material = DrawTool.LineMatWhite;
    lr.startColor=color;
    lr.endColor = color;
    lr.startWidth = duration;
    lr.endWidth = duration;
    lr.SetPosition(0, start);
    lr.SetPosition(1, end);
    }
    public static void DrawLine(Vector3[] positions, Color color, float duration = 0.02f)
    {
    for(int i = 0; i < positions.Length-1;i++)
    {
        DrawLine(positions[i],positions[i+1],Color.green);
    }
    }
    private static GameObject GetOrInstance_GObjAsGiz(string n,Vector3 trans,Quaternion qua, params System.Type[] components)
    {
        GameObject go;
        if(!Gizs.ContainsKey(n))
        {
            if(n=="drawline")
            {
                go = new GameObject(n,typeof(LineRenderer));
                go.transform.SetParent(Gizs[ParentGizTool].transform);
                Gizs.Add(n,go);
                go.transform.position = trans;
                return go;
            }
            go = new GameObject(n,components);
            go.transform.SetParent(Gizs[ParentGizTool].transform);
            Gizs.TryAdd(n,go);
            go.transform.position = trans;
            return go;
        }
         
        go = GameObject.Instantiate(Gizs[n],trans,qua);
        return go;

    }
    private static Dictionary<string,GameObject> Gizs = new Dictionary<string, GameObject>()
    {
        {
            "GizsTool",
            new GameObject("GizsTool")
        }
    };
    public static readonly string ParentGizTool = "GizsTool";
}
