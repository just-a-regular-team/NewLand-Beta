using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaterialDB
{
    public static Material MatFrom(string texPath, bool reportFailure)
    {
        if (texPath == null || texPath == "null")
        {
            return null;
        }
        return MatFrom(new MaterialRequest(FindContent<Texture2D>.Get(texPath, reportFailure)));
    }

    public static Material MatFrom(string texPath)
    {
        if (texPath == null || texPath == "null")
        {
            return null;
        }
        return MatFrom(new MaterialRequest(FindContent<Texture2D>.Get(texPath, true)));
    }

    public static Material MatFrom(Texture2D srcTex)
    {
        return MatFrom(new MaterialRequest(srcTex));
    }

    public static Material MatFrom(Texture2D srcTex, Shader shader, Color color)
    {
        return MatFrom(new MaterialRequest(srcTex, shader, color));
    }

    public static Material MatFrom(Texture2D srcTex, Shader shader, Color color, int renderQueue)
    {
        return MatFrom(new MaterialRequest(srcTex, shader, color)
        {
            renderQueue = renderQueue
        });
    }

    public static Material MatFrom(string texPath, Shader shader)
    {
        return MatFrom(new MaterialRequest(FindContent<Texture2D>.Get(texPath, true), shader));
    }

    public static Material MatFrom(string texPath, Shader shader, int renderQueue)
    {
        return MatFrom(new MaterialRequest(FindContent<Texture2D>.Get(texPath, true), shader)
        {
            renderQueue = renderQueue
        });
    }

    public static Material MatFrom(string texPath, Shader shader, Color color)
    {
        return MatFrom(new MaterialRequest(FindContent<Texture2D>.Get(texPath, true), shader, color));
    } 
    public static Material MatFrom(string texPath, Shader shader, Color color, int renderQueue)
    {
        return MatFrom(new MaterialRequest(FindContent<Texture2D>.Get(texPath, true), shader, color)
        {
            renderQueue = renderQueue
        });
    } 
    public static Material MatFrom(Shader shader)
    {
        return MatFrom(new MaterialRequest(shader));
    } 
    public static Material MatFrom(MaterialRequest req)
    {
            
        if (req.mainTex == null && req.needsMainTex)
        {
            Debug.LogError("MatFrom with null sourceTex.");
            return DefaultContent.BadMat;
        }
        if (req.shader == null)
        {
            Debug.LogWarning("Matfrom with null shader.");
            return DefaultContent.BadMat;
        }
        if (req.maskTex != null )
        {
            Debug.LogError("MaterialRequest has maskTex but shader does not support it. req=" + req.ToString());
            req.maskTex = null;
        }
        Material material;
        if (!matDictionary.TryGetValue(req, out material))
        {
            material = new Material(req.shader);
            material.name = req.shader.name;
            if (req.mainTex != null)
            {
                Material material2 = material;
                material2.name = material2.name + "_" + req.mainTex.name;
                material.mainTexture = req.mainTex;
            }
            material.color = req.color;
            if (req.maskTex != null)
            {
                material.SetTexture(ShaderPropety.PropertyToID(ShaderPropety.MaskTexName), req.maskTex);
                material.SetColor(ShaderPropety.PropertyToID(ShaderPropety.ColorTwoName), req.colorTwo);
            }
            if (req.renderQueue != 0)
            {
                material.renderQueue = req.renderQueue;
            }
                
            matDictionary.Add(req, material);
            matDictionaryReverse.Add(material, req);
        }
        return material;
    }

    public static bool TryGetRequestForMat(Material material, out MaterialRequest request)
    {
        return matDictionaryReverse.TryGetValue(material, out request);
    } 
    private static Dictionary<MaterialRequest, Material> matDictionary = new Dictionary<MaterialRequest, Material>(); 
    private static Dictionary<Material, MaterialRequest> matDictionaryReverse = new Dictionary<Material, MaterialRequest>();
}
