using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MaterialRequest
    {
    public MaterialRequest(Texture tex)
    {
        this.shader = null;
        this.mainTex = tex;
        this.color = Color.white;
        this.colorTwo = Color.white;
        this.maskTex = null;
        this.renderQueue = 0;
        this.needsMainTex = true;
    }
    public MaterialRequest(Texture tex, Shader shader)
    {
        this.shader = shader;
        this.mainTex = tex;
        this.color = Color.white;
        this.colorTwo = Color.white;
        this.maskTex = null;
        this.renderQueue = 0;
        this.needsMainTex = true;
    }
    public MaterialRequest(Texture tex, Shader shader, Color color)
    {
        this.shader = shader;
        this.mainTex = tex;
        this.color = color;
        this.colorTwo = Color.white;
        this.maskTex = null;
        this.renderQueue = 0;
        this.needsMainTex = true;
    }
    public MaterialRequest(Shader shader)
    {
        this.shader = shader;
        this.mainTex = null;
        this.color = Color.white;
        this.colorTwo = Color.white;
        this.maskTex = null;
        this.renderQueue = 0;
        this.needsMainTex = false;
    }

    public Shader shader; 
    public Texture mainTex; 
    public Color color; 
    public Color colorTwo;  
    public Texture2D maskTex; 
    public int renderQueue; 
    public bool needsMainTex;
    }
