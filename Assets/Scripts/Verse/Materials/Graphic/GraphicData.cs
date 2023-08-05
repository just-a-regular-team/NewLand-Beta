using System;
using UnityEngine;

public sealed class GraphicData
{
    public int renderQueue;
    public Type graphic;
    public bool usedAtlas;
    public string textPath;
    public Color color = Color.white;
    public Vector2 drawSize = Vector2.one;
    public Vector2 offSet = Vector2.zero;
    public Shader shader;

    private Graphic cachedGraphic;//Remember ignore it when save or load

    public Graphic TryFindGraphicFor()
    {
        return this.Graphic;
    }



    private void Init()
    {
        if (this.graphic == null)
        {
            this.cachedGraphic = null;
            return;
        }
        this.cachedGraphic = GraphicDB.Get(graphic,textPath,shader,drawSize,color,this);
    }
    public void ExplicitlyInitCachedGraphic()
    {
        this.cachedGraphic = this.Graphic;
    }
    public Graphic Graphic
    {
        get
        {
            if (this.cachedGraphic == null)
            {
                this.Init();
            }
            return this.cachedGraphic;
        }
    }
}
