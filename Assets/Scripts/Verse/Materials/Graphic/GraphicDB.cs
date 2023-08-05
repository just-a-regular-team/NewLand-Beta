using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class GraphicDB
{
    public static Graphic Get<T>(string path, Shader shader) where T : Graphic, new()
    {
        return GetInner<T>(new GraphicRequest(typeof(T), path, shader, Vector2.one, Color.white, null, 0));
    }
    public static Graphic Get<T>(string path, Shader shader, Vector2 drawSize, Color color) where T : Graphic, new()
    {
        return GetInner<T>(new GraphicRequest(typeof(T), path, shader, drawSize, color, null, 0));
    }
    public static Graphic Get<T>(
                                 string path,
                                 Shader shader,
                                 Vector2 drawSize,
                                 Color color,
                                 int renderQueue) where T : Graphic, new()
    {
        return GetInner<T>(new GraphicRequest(typeof(T), path, shader, drawSize, color,null, renderQueue));
    }
    public static Graphic Get<T>(
                                 Texture2D texture,
                                 Shader shader,
                                 Vector2 drawSize,
                                 Color color,
                                 int renderQueue) where T : Graphic, new()
    {
        return GetInner<T>(new GraphicRequest(typeof(T), texture, shader, drawSize, color,  null, renderQueue));
    }
    public static Graphic Get<T>(
                                 Texture2D texture,
                                 Shader shader,
                                 Vector2 drawSize,
                                 Color color) where T : Graphic, new()
    {
        return GetInner<T>(new GraphicRequest(typeof(T), texture, shader, drawSize, color,null, 0));
    }
    public static Graphic Get<T>(string path,
                                 Shader shader,
                                 Vector2 drawSize,
                                 Color color,
                                 GraphicData graphicData) where T : Graphic, new()
    {
        return GetInner<T>(new GraphicRequest(typeof(T), path, shader, drawSize, color, graphicData, 0));
    }
    public static Graphic Get(Type graphicClass,
                                 string path,
                                 Shader shader,
                                 Vector2 drawSize,
                                 Color color,
                                 GraphicData graphicData) 
    {
        return GraphicDB.Get(new GraphicRequest(graphicClass, path, shader, drawSize, color,  graphicData, 0));
    }
    public static Graphic Get(Type graphicClass,
                                 Texture2D texture,
                                 Shader shader,
                                 Vector2 drawSize,
                                 Color color,
                                 GraphicData graphicData)
    {
        return GraphicDB.Get(new GraphicRequest(graphicClass, texture, shader, drawSize, color,  graphicData, 0));
    }
    private static Graphic Get(GraphicRequest req)
    {
        try
        {
            Func<GraphicRequest, Graphic> func;
            if (!GraphicDB.cachedGraphicGetters.TryGetValue(req.graphicClass, out func))
            {
                MethodInfo method = typeof(GraphicDB).GetMethod("GetInner", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
                {
                    req.graphicClass
                });
                func = (Func<GraphicRequest, Graphic>)Delegate.CreateDelegate(typeof(Func<GraphicRequest, Graphic>), method);
                GraphicDB.cachedGraphicGetters.Add(req.graphicClass, func);
            }
            return func(req);
        }
        catch (Exception ex)
        {
            Debug.LogError(string.Concat(new object[]
            {
                "Exception getting ",
                req.graphicClass,
                " at ",
                req.path,
                ": ",
                ex.ToString()
            }));
        }
        return null;
        //return BaseContent.BadGraphic;
    }
    private static T GetInner<T>(GraphicRequest req) where T : Graphic, new()
    {
    
        req.renderQueue = ((req.renderQueue == 0 && req.graphicData != null) ? req.graphicData.renderQueue : req.renderQueue);
        if (!GraphicDB.allGraphics.TryGetValue(req, out Graphic graphic))
        {
            graphic = Activator.CreateInstance<T>();
            graphic.Init(req);
            GraphicDB.allGraphics.Add(req, graphic);
        }
        return (T)((object)graphic);
    }
    public static void Clear()
    {
        GraphicDB.allGraphics.Clear();
    }

    public static void AllGraphicsLoaded()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("There are " + GraphicDB.allGraphics.Count + " graphics loaded.");
        int num = 0;
        foreach (Graphic graphic in GraphicDB.allGraphics.Values)
        {
            stringBuilder.AppendLine(num + " - " + graphic.ToString());
            if (num % 50 == 49)
            {
                Debug.Log(stringBuilder.ToString());
                stringBuilder = new StringBuilder();
            }
            num++;
        }
        Debug.Log(stringBuilder.ToString());
    }
    private static Dictionary<GraphicRequest, Graphic> allGraphics = new();
    private static Dictionary<Type, Func<GraphicRequest, Graphic>> cachedGraphicGetters = new();
}
