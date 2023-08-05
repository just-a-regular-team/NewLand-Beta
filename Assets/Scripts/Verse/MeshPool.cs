
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshPool
{
    public static Mesh GridPlane(Vector2 size)
    {
        Mesh mesh;
        if (!planes.TryGetValue(size, out mesh))
        {
            mesh = NewPlaneMesh(size, false, false, false);
            planes.Add(size, mesh);
        }
        return mesh;
    }
    public static Mesh GridPlaneFlip(Vector2 size)
    {
        Mesh mesh;
        if (!planesFlip.TryGetValue(size, out mesh))
        {
            mesh = NewPlaneMesh(size, true, false, false);
            planesFlip.Add(size, mesh);
        }
        return mesh;
    }
    private static Mesh NewPlaneMesh(float size)
    {
        return NewPlaneMesh(size, false);
    }
    private static Mesh NewPlaneMesh(float size, bool flipped)
    {
        return NewPlaneMesh(size, flipped, false);
    }
    private static Mesh NewPlaneMesh(float size, bool flipped, bool backLift)
    {
        return NewPlaneMesh(new Vector2(size, size), flipped, backLift, false);
    }
    private static Mesh NewPlaneMesh(float size, bool flipped, bool backLift, bool twist)
    {
        return NewPlaneMesh(new Vector2(size, size), flipped, backLift, twist);
    }

    private static Mesh NewPlaneMesh(Vector2 size, bool flipped, bool backLift, bool twist)
    {
        Vector3[] array = new Vector3[4];
        Vector2[] array2 = new Vector2[4];
        int[] array3 = new int[6];
        array[0] = new Vector3(-0.5f * size.x,-0.5f * size.y);
        array[1] = new Vector3(-0.5f * size.x,0.5f * size.y);
        array[2] = new Vector3(0.5f * size.x,0.5f * size.y);
        array[3] = new Vector3(0.5f * size.x,-0.5f * size.y);
        if (backLift)
        {
            array[1].y = 0.002027027f;
            array[2].y = 0.002027027f;
            array[3].y = 0.00081081083f;
        }
        if (twist)
        {
            array[0].y = 0.0010135135f;
            array[1].y = 0.00050675677f;
            array[2].y = 0f;
            array[3].y = 0.00050675677f;
        }
        if (!flipped)
        {
            array2[0] = new Vector2(0f, 0f);
            array2[1] = new Vector2(0f, 1f);
            array2[2] = new Vector2(1f, 1f);
            array2[3] = new Vector2(1f, 0f);
        }
        else
        {
            array2[0] = new Vector2(1f, 0f);
            array2[1] = new Vector2(1f, 1f);
            array2[2] = new Vector2(0f, 1f);
            array2[3] = new Vector2(0f, 0f);
        }
        array3[0] = 0;
        array3[1] = 1;
        array3[2] = 2;
        array3[3] = 0;
        array3[4] = 2;
        array3[5] = 3;
        Mesh mesh = new Mesh();
        mesh.name = "NewPlaneMesh()";
        mesh.vertices = array;
        mesh.uv = array2;
        mesh.SetTriangles(array3, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }
    public static readonly Mesh plane025 = MeshPool.NewPlaneMesh(0.25f);
    public static readonly Mesh plane03 = MeshPool.NewPlaneMesh(0.3f);
    public static readonly Mesh plane05 = MeshPool.NewPlaneMesh(0.5f);
    public static readonly Mesh plane08 = MeshPool.NewPlaneMesh(0.8f);
    public static readonly Mesh plane10 = MeshPool.NewPlaneMesh(1f);
    public static readonly Mesh plane10Flip = MeshPool.NewPlaneMesh(1f, true);
    public static readonly Mesh plane14 = MeshPool.NewPlaneMesh(1.4f);
    public static readonly Mesh plane20 = MeshPool.NewPlaneMesh(2f);

    private static Dictionary<Vector2, Mesh> planes = new Dictionary<Vector2, Mesh>();
	private static Dictionary<Vector2, Mesh> planesFlip = new Dictionary<Vector2, Mesh>();
}
