using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShaderDB
{
    public static Shader DefaultShader
    {
        get
        {
            return ShaderDB.DF_Shader;
        }
    }
    public static Shader LoadShader(string shaderPath,bool findOut = true)
    {
        if (lookup == null)
        {
            lookup = new Dictionary<string, Shader>();
        }
        if (!lookup.ContainsKey(shaderPath))
        {
            lookup[shaderPath] = (Shader)Resources.Load(FilePath.ContentPath<Shader>() + shaderPath, typeof(Shader));
        }
        Shader shader = lookup[shaderPath];
        if (shader == null)
        {
            if(findOut)
            {
                lookup[shaderPath] = Shader.Find(FilePath.ContentPath<Shader>()+shaderPath);
            }
            Debug.LogWarning("Could not load shader " + shaderPath);
            if(findOut)
            {
                Debug.LogWarning("Could not load shader " + FilePath.ContentPath<Shader>()+shaderPath);
            }
            return DefaultShader;
        }
        return shader;
    }
    private static Dictionary<string, Shader> lookup;

    public static readonly Shader DF_Shader = LoadShader("Shader/DefaultShader");
}
