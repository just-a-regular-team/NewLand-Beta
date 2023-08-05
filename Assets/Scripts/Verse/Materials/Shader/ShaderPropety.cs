using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShaderPropety
{
    public readonly static Dictionary<string,int> ShaderPropetyIDs = new Dictionary<string,int>();
    public static int PropertyToID(string ShaderPropety)
    {
        int result = Shader.PropertyToID(ShaderPropety);
        if(ShaderPropetyIDs.ContainsKey(ShaderPropety))
        {
            ShaderPropetyIDs.TryAdd(ShaderPropety,result);
        }
        return result;
    }


    public static readonly string ColorName = "_Color";
    public static readonly string MaskTexName = "_MaskTex";
    public static readonly string ColorTwoName = "_ColorTwo";
}
