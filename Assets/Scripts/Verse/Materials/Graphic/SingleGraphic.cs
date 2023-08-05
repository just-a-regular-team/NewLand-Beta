using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGraphic : Graphic
{
    public override void Init(GraphicRequest req)
    {
        mat = DefaultContent.BadMat;
    }
    public override Material MatAt(Thing thing)
    {
        return mat;
    }
    protected Material mat;
}
