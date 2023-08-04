using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : Entity
{
    public Map Map
    {
        get{return Map.MapWorking;}
    }
    public override string Name { get => name;set {name = value;} }
    

    public override void SpawnSetup(Map map)
    {
        if(map != Map.MapWorking)
        {
            Debug.Log("map select is not working!");
            return;
        }
        obj = new GameObject(name);
        obj.transform.position = this.position;
        Current.GetGamePlaying.GetTimeController.RegisterAllTickabilityFor(this);
        Map.DrawThingController.RegisterThingToDraw(this);
        Destroyed = false;
        
    }

    public override void DeSpawn()
    {
        Current.GetGamePlaying.GetTimeController.DeRegisterAllTickabilityFor(this);
        Map.DrawThingController.DeregisterThingToDraw(this);
        Destroyed = true;
    }

    public virtual void Destroy()
    {
        Current.GetGamePlaying.GetTimeController.DeRegisterAllTickabilityFor(this);
        Map.DrawThingController.DeregisterThingToDraw(this);
        Destroyed = true;
    }

    public override void Tick()
    {
        Debug.Log(position);
    }

    //Draw thing everyframe by drawThing
    public virtual void Draw()
    {
        DrawThing(position);
    }

    //Maybe update this shit func
    public virtual void DrawThing(Vector3 pos)
    {
        var mesh = new Mesh {
			name = "Procedural Mesh"
		};

		mesh.vertices = new Vector3[] {
			Vector3.zero, Vector3.right, Vector3.up
		};
        mesh.triangles = new int[] {
			0, 2, 1
		};
        Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, null, 0);
    }

    public int thingIDNumber = -1;// Id of thing like 000001->999999
    public int globalID;//Id of thing in map like Thing_"ID_On_Map"
    public string name;

    public DataOfThing data;

    public bool Destroyed;

    public Vector3 position;
    public GameObject obj;
}
