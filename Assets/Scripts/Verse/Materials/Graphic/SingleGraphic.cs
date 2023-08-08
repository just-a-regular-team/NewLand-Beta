using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGraphic : Graphic
{
    public override void Init(GraphicRequest req)
    {
        //mat = DefaultContent.BadMat;
        mesh = MeshPool.plane08;
    }
    public override Material Mat(Thing thing)
    {
        return mat;
    }
    public override Mesh Mesh(Thing thing)
    {
        return mesh;
    }
    protected Material mat;
    protected Mesh mesh;
}
