using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefaultContent
{
    public static readonly string BadTexPath = "Unvalid";
    public static readonly Material BadMat = MaterialDB.MatFrom(DefaultContent.BadTexPath, ShaderDB.DefaultShader);
    public static readonly Material LineMatWhite = MaterialDB.MatFrom(DefaultContent.BadTexPath, ShaderDB.DefaultShader, Color.black);
}
