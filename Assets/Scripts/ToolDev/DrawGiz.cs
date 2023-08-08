using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawGiz
{
    static DrawGiz()
    {
        Gizs.Add(ParentGizTool.name,ParentGizTool);
    }
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.02f)
    {
    
        GameObject myLine = GetOrInstance_GObjAsGiz("drawline",Quaternion.identity);
        ListWorking.Add(myLine);
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        //lr.material = DrawTool.LineMatWhite;
        lr.material = DefaultContent.LineMatWhite;
        lr.startColor=color;
        lr.endColor = color;
        lr.startWidth = duration;
        lr.endWidth = duration;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
    public static void DrawWireSphere(Vector3 center,int steps,float rad,float duration,Color color)
    {
    
        GameObject drawwiresphere = GetOrInstance_GObjAsGiz("drawwiresphere",Quaternion.identity,typeof(LineRenderer));
        ListWorking.Add(drawwiresphere);
        LineRenderer lr = drawwiresphere.GetComponent<LineRenderer>();
        lr.material = DefaultContent.LineMatWhite;
        lr.startColor=color;
        lr.endColor = color;
        lr.startWidth = duration;
        lr.endWidth = duration;
        lr.loop = true; 
        lr.positionCount = steps;
        for(int currStep = 0; currStep < steps; currStep++)
        {
            float circumferenceProgress = (float)currStep/steps;
            float currentRad = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRad);
            float yScaled = Mathf.Sin(currentRad);

            float x = xScaled * rad;
            float y = yScaled * rad;

            Vector3 currPos = new Vector3(x,y,0) + center;
            lr.SetPosition(currStep,currPos);
        }
    }
    
    public static void Clear()
    {
        while(ListWorking.Count>0)
        {
            GameObject go = ListWorking[0];
            ListWorking.RemoveAt(0);
            GameObject.Destroy(go);
        }
    }
    public static void DrawLine(Vector3[] positions, Color color, float duration = 0.02f)
    {
    for(int i = 0; i < positions.Length-1;i++)
    {
        DrawLine(positions[i],positions[i+1],Color.green);
    }
    }
    private static GameObject GetOrInstance_GObjAsGiz(string n,Quaternion qua, params System.Type[] components)
    {
        GameObject go;
        if(!Gizs.ContainsKey(n))
        {
            if(n=="drawline")
            {
                go = new GameObject(n,typeof(LineRenderer));
                Gizs.Add(n,go);
                go.transform.position = Vector2.zero;
                go.transform.SetParent(ParentGizTool.transform);
                goto TheEnd;
            }
            go = new GameObject(n,components);
            Gizs.TryAdd(n,go);
            go.transform.position = Vector2.zero;
            go.transform.SetParent(ParentGizTool.transform);
            goto TheEnd;
        }
        TheEnd:;

        go = GameObject.Instantiate(Gizs[n],Vector2.zero,qua);
        go.transform.SetParent(ParentGizTool.transform);
        return go;

    }
    public static List<GameObject> ListWorking = new List<GameObject>();
    private static Dictionary<string,GameObject> Gizs = new Dictionary<string, GameObject>();


    public static readonly GameObject ParentGizTool = new GameObject("GizsTool");
}
