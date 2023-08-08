using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphic
{
    public virtual Material Mat(Thing thing = null)
    {
        return null;
    }
    public virtual Mesh Mesh(Thing thing = null)
    {
        return null;
    }
    public virtual void Init(GraphicRequest req)
    {
        Debug.Log("Cannot init Graphic of class " + base.GetType().ToString());
    }
    public void DrawMesh(Thing thing)
    {
        Material mat = Mat(thing);
        Mesh mesh = Mesh(thing);
        DrawMesh(mesh,thing.position,thing.obj.transform.rotation,mat);
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
