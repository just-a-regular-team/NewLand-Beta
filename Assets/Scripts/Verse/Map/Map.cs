using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
     string mapName;
    int mapSize;
    int sizeChunk;
    public int sizeRadiusOfChunk = 3;
    Map(){}
    public Map(string n,int size)
    {
        mapName = n;
        mapSize = size;
        sizeChunk = 5;
    }

    private void ConstructComps()
    {
        DrawThingController = new DrawThingController(this);
    }
    
    public Map CreateNewMap()
    {
        Map m = null;
        try
        {
           
            m = new Map(mapName,mapSize);
            m.ConstructComps();
            for(int x = -(mapSize);x<mapSize;x++)
            {
                for(int y = -(mapSize);y<mapSize;y++)
                {
                    GameObject chunk = Chunk.CreateChunk(new Vector2Int(x,y),sizeChunk,m);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }
        finally
        {
            MapWorking = m;
        }
        return m;
        
    }
    public void LoadMap(Map map)
    {
        
    }
    public void MapUpdate()
    {
        DrawThingController.UpdateDrawThing();
    }
    

    public DrawThingController DrawThingController{get;private set;}


    public static Map MapWorking;
}
