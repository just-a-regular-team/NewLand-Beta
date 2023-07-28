using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager
{
    public SpriteManager()
    {
        LoadAllSpriteDefault(); 
    }
    private  Dictionary<string,Sprite> spriteAssets = new Dictionary<string, Sprite>();
    public  Sprite GetSprite(string texPath)
    {
        return spriteAssets[texPath];
    }
    
    public  void LoadAllSpriteDefault()
    {
        foreach(string contentPath in FindContent<Texture2D>.ListAllContent.Keys)
        {
            Texture2D tex = FindContent<Texture2D>.ListAllContent[contentPath].t;
            Sprite sprite = LoadSprite(tex,new Rect(0,0,tex.width,tex.height),64);
            spriteAssets.Add(contentPath,sprite);
        }
    }
    Sprite LoadSprite(Texture2D imageTexture, Rect spriteCoordinates, int pixelsPerUnit) {
		//Debug.Log("LoadSprite: " + spriteName);
		Vector2 pivotPoint = new Vector2(0.5f, 0.5f);	// Ranges from 0..1 -- so 0.5f == center

		Sprite s = Sprite.Create(imageTexture, spriteCoordinates, pivotPoint, pixelsPerUnit);

		return s;
	}
}
