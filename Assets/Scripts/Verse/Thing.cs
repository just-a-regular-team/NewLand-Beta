using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : Entity
{
    public override string Name { get => name;set {name = value;} }
    

    public override void SpawmSetup(Map map)
    {
        if(map != Map.MapWorking)
        {
            Debug.Log("map select is not working!");
            return;
        }
        
    }

    public override void Despawm()
    {
        
    }

   

    public int thingIDNumber = -1;// Id of thing like 000001->999999
    public int globalID;//Id of thing in map like Thing_"ID_On_Map"
    public string name;

    public DataOfThing data;
}
