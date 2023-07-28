using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    int X,Y,Z;
    public Chunk chunk{get;private set;}
    public GameObject tileObj;
     

    public Tile(int x,int y, int z,Chunk c)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.chunk = c;
    }
    public Tile(int x,int y,Chunk c)
    {
        this.X = x;
        this.Y = y;
        this.Z = 0;
        this.chunk = c;

        tileObj = CreatetileObj();
    }

    private GameObject CreatetileObj()
    {
        GameObject title_obj = new GameObject($"Tile({getX},{getY})"); 
        title_obj.transform.position = new Vector3(getX+0.5f,getY+0.5f,getZ);
        
        title_obj.transform.SetParent(chunk.gameObject.transform,true);

        SpriteRenderer sr = title_obj.AddComponent<SpriteRenderer>();
 		sr.sprite = Controller.SpriteManager.GetSprite("Tile/Untitle.png");

        return title_obj;
    }
    
    public int getX {get{return X;}}
    public int getY {get{return Y;}}
    public int getZ {get{return Z;}}

    public int setX {set{X = value;}}
    public int setY {set{Y = value;}}
    public int setZ {set{Z = value;}}
}
