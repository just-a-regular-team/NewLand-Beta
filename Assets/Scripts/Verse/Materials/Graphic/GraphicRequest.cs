using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicRequest : IEquatable<GraphicRequest>
{
    public GraphicRequest(Type graphicClass, string path, Shader shader, Vector2 drawSize, Color color, GraphicData graphicData, int renderQueue)
    {
        this.graphicClass = graphicClass;
        this.path = path;
        this.shader = shader;
        this.drawSize = drawSize;
        this.color = color;
        this.graphicData = graphicData;
        this.renderQueue = renderQueue;
        this.texture = null;
    }
    public GraphicRequest(Type graphicClass, Texture2D texture, Shader shader, Vector2 drawSize, Color color, GraphicData graphicData, int renderQueue)
    {
        this.graphicClass = graphicClass;
        this.texture = texture;
        this.shader = shader;
        this.drawSize = drawSize;
        this.color = color;
        this.graphicData = graphicData;
        this.renderQueue = renderQueue;
        this.path = null;
    }
    public override bool Equals(object obj)
    {
        return obj is GraphicRequest request && this.Equals(request);
    }
    public bool Equals(GraphicRequest other)
    {
        return this.graphicClass == other.graphicClass &&
         this.path == other.path && this.texture == other.texture && 
         this.shader == other.shader && this.drawSize == other.drawSize && 
         this.color == other.color&& this.graphicData == other.graphicData && 
         this.renderQueue == other.renderQueue;
    }
    public static bool operator ==(GraphicRequest lhs, GraphicRequest rhs)
    {
        return lhs.Equals(rhs);
    }
    public static bool operator !=(GraphicRequest lhs, GraphicRequest rhs)
    {
        return !(lhs == rhs);
    }


    public Type graphicClass;
    public Texture2D texture;
    public string path;
    public Shader shader;
    public Vector2 drawSize;
    public Color color;
    public GraphicData graphicData;
    public int renderQueue;
}
