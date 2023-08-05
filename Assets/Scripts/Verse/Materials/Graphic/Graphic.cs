using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphic
{
    public virtual Material MatAt(Thing thing = null)
    {
        return null;
    }
    public virtual void Init(GraphicRequest req)
    {
        Debug.Log("Cannot init Graphic of class " + base.GetType().ToString());
    }
    public void DrawMesh(Thing thing)
    {
        Material mat = MatAt(thing);
        Mesh mesh = MeshPool.plane10;
        DrawMesh(mesh,thing.position,Quaternion.identity,mat);
    }
    public void DrawMesh(Mesh mesh,Vector3 pos,Quaternion rot,Material mat,int layer = 0)
    {
        Graphics.DrawMesh(mesh,pos,rot,mat,layer);
    }
    public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material mat, int layer = 0)
    {
        Graphics.DrawMesh(mesh,matrix,mat,layer);
    }
}
