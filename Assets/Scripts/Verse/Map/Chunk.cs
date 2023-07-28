using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{    
    public int width{get;private set;}
    public int height{get;private set;}
    public Vector2Int chunkWorldPosition;
    public int radianDist = 30;
    
    Action<Chunk> cbChunkWhenUpdate;

    bool isEnable;
    public bool Enable
    {
        get {return isEnable;}
        set 
        {
            bool oldBool = isEnable;
            isEnable = value;
            if(cbChunkWhenUpdate != null && oldBool != isEnable)
            {
                cbChunkWhenUpdate(this);
            }
        }
    }

    public Tile[,] StorageTiles{get;private set;}
    public Map map{get;private set;}
    public static GameObject CreateChunk(Vector2Int pos,int sizeChunk,Map m)
    {
        GameObject chunkObj = new GameObject($"Chunk_[{pos.x}|{pos.y}]");
        chunkObj.transform.position = new Vector3(pos.x*sizeChunk,pos.y*sizeChunk);

        Chunk chunk = chunkObj.AddComponent<Chunk>();
        chunk.chunkWorldPosition = pos;
        chunk.width = sizeChunk;
        chunk.height = sizeChunk;

        chunk.StorageTiles = new Tile[chunk.width,chunk.height];
        chunk.map = m;
        return chunkObj;
    }
    void CreatTilesInChunk()
    {
        for(int x=0;x<StorageTiles.GetLength(0);x++)
        {
            for(int y=0;y<StorageTiles.GetLength(1);y++)
            {
                Tile tile = new Tile((chunkWorldPosition.x*width)+x,(chunkWorldPosition.y*height)+y,this);
                StorageTiles[x,y] = tile;
            }
        }
    }
    void Awake()
    {
         
    }
    void Start()
    {
        Enable = false;
        cbChunkWhenUpdate = cbRenderMode;
        CreatTilesInChunk();
    }
    void Update()
    {
         if (getDist(GameObject.Find("Main Camera").transform.position, this.transform.position + new Vector3(width / 2, height / 2, 0.0f)) < radianDist)
         {
            Enable = true;
         }
         else
         {
            Enable = false;
         }
    }

    float getDist(Vector3 p1, Vector3 p2)
    {
        Vector3 vectorDist = p2 - p1;
        return Mathf.Sqrt(vectorDist.x * vectorDist.x + vectorDist.y * vectorDist.y);
    }
    void cbRenderMode(Chunk chunk)
    {
       for(int x=0;x<StorageTiles.GetLength(0);x++)
        {
            for(int y=0;y<StorageTiles.GetLength(1);y++)
            {
                 
                Tile tile = StorageTiles[x,y];
                tile.tileObj.SetActive(Enable);
            }
        }
    }
}
