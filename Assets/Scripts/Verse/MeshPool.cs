
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshPool
{
    static MeshPool()
    {
        for (int i = 0; i < 361; i++)
        {
            MeshPool.pies[i] = MeshPool.MakePieMesh(i);
        }
    }
    public static Mesh GridPlane(Vector2 size)
    {
        if (!planes.TryGetValue(size, out Mesh mesh))
        {
            mesh = NewPlaneMesh(size, false, false, false);
            planes.Add(size, mesh);
        }
        return mesh;
    }
    public static Mesh GridPlaneFlip(Vector2 size)
    {
        if (!planesFlip.TryGetValue(size, out Mesh mesh))
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
        Mesh mesh = new();
        mesh.name = "NewPlaneMesh()";
        mesh.vertices = array;
        mesh.uv = array2;
        mesh.SetTriangles(array3, 0);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    public static Mesh MakeCircleMesh(float radius,int segments = 360)
    {
        Mesh circleMesh = new Mesh();
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        float angleStep = 2 * Mathf.PI / segments;
        float angle = 0f;

        for (int i = 0; i < segments; i++)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            vertices[segments-i] = new Vector3(x, y, 0f);

            if (i > 0)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = segments - i;
                triangles[i * 3 + 2] = segments - i + 1;
            }

            angle += angleStep;
        }

        vertices[segments] = vertices[0];
        circleMesh.name = "MakeCircleMesh()";
        circleMesh.vertices = vertices;
        circleMesh.triangles = triangles;
        circleMesh.RecalculateNormals();
        return circleMesh;
    }
    
    public static Mesh MakePieMesh(int DegreesWide)
    {
        List<Vector2> list = new List<Vector2>();
        list.Add(new Vector2(0f, 0f));
        for (int i = 0; i < DegreesWide; i++)
        {
            float num = (float)i / 180f * 3.1415927f;
            list.Add(new Vector2(0f, 0f)
            {
                x = (float)(0.550000011920929 * Math.Cos((double)num)),
                y = (float)(0.550000011920929 * Math.Sin((double)num))
            });
        }
        Vector3[] array = new Vector3[list.Count];
        for (int j = 0; j < array.Length; j++)
        {
            array[j] = new Vector3(list[j].x, list[j].y);
        }
        int[] triangles = new Triangulator(list.ToArray()).Triangulate();
        Mesh mesh = new Mesh();
        mesh.name = "MakePieMesh()";
        mesh.vertices = array;
        mesh.uv = new Vector2[list.Count];
        mesh.triangles = triangles;
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

    public static readonly Mesh circle = MeshPool.MakeCircleMesh(0.5f);
    public static readonly Mesh[] pies = new Mesh[361];

    private static Dictionary<Vector2, Mesh> planes = new Dictionary<Vector2, Mesh>();
	private static Dictionary<Vector2, Mesh> planesFlip = new Dictionary<Vector2, Mesh>();


    public struct Triangulator
	{
		public Triangulator(Vector2[] points)
		{
            m_points = new List<Vector2>();
			this.m_points = new List<Vector2>(points);
		}
		public readonly int[] Triangulate()
		{
			List<int> list = new List<int>();
			int count = this.m_points.Count;
			if (count < 3)
			{
				return list.ToArray();
			}
			int[] array = new int[count];
			if (this.Area() > 0f)
			{
				for (int i = 0; i < count; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					array[j] = count - 1 - j;
				}
			}
			int k = count;
			int num = 2 * k;
			int num2 = 0;
			int num3 = k - 1;
			while (k > 2)
			{
				if (num-- <= 0)
				{
					return list.ToArray();
				}
				int num4 = num3;
				if (k <= num4)
				{
					num4 = 0;
				}
				num3 = num4 + 1;
				if (k <= num3)
				{
					num3 = 0;
				}
				int num5 = num3 + 1;
				if (k <= num5)
				{
					num5 = 0;
				}
				if (this.Snip(num4, num3, num5, k, array))
				{
					int item = array[num4];
					int item2 = array[num3];
					int item3 = array[num5];
					list.Add(item);
					list.Add(item2);
					list.Add(item3);
					num2++;
					int num6 = num3;
					for (int l = num3 + 1; l < k; l++)
					{
						array[num6] = array[l];
						num6++;
					}
					k--;
					num = 2 * k;
				}
			}
			list.Reverse();
			return list.ToArray();
		}
		private readonly float Area()
		{
			int count = this.m_points.Count;
			float num = 0f;
			int index = count - 1;
			int i = 0;
			while (i < count)
			{
				Vector2 vector = this.m_points[index];
				Vector2 vector2 = this.m_points[i];
				num += vector.x * vector2.y - vector2.x * vector.y;
				index = i++;
			}
			return num * 0.5f;
		}
		private readonly bool Snip(int u, int v, int w, int n, int[] V)
		{
			Vector2 vector = this.m_points[V[u]];
			Vector2 vector2 = this.m_points[V[v]];
			Vector2 vector3 = this.m_points[V[w]];
			if (Mathf.Epsilon > (vector2.x - vector.x) * (vector3.y - vector.y) - (vector2.y - vector.y) * (vector3.x - vector.x))
			{
				return false;
			}
			for (int i = 0; i < n; i++)
			{
				if (i != u && i != v && i != w)
				{
					Vector2 p = this.m_points[V[i]];
					if (this.InsideTriangle(vector, vector2, vector3, p))
					{
						return false;
					}
				}
			}
			return true;
		}
		private readonly bool InsideTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
		{
			float num = C.x - B.x;
			float num2 = C.y - B.y;
			float num3 = A.x - C.x;
			float num4 = A.y - C.y;
			float num5 = B.x - A.x;
			float num6 = B.y - A.y;
			float num7 = P.x - A.x;
			float num8 = P.y - A.y;
			float num9 = P.x - B.x;
			float num10 = P.y - B.y;
			float num11 = P.x - C.x;
			float num12 = P.y - C.y;
			float num13 = num * num10 - num2 * num9;
			float num14 = num5 * num8 - num6 * num7;
			float num15 = num3 * num12 - num4 * num11;
			return num13 >= 0f && num15 >= 0f && num14 >= 0f;
		}
		private List<Vector2> m_points;
	}
}
