using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoMenuEntry
{
    public static void MainMenuOnGUI()
    {

        GUI.DrawTexture(new Rect(0,0,UI.screenWidth,UI.screenHeight), BackGround, ScaleMode.ScaleToFit);
        
        Rect rect = new Rect((float)(UI.screenWidth / 2) - (PaneSize.x / 2f), (float)(UI.screenHeight / 2) - (PaneSize.y / 2f) + 50f, PaneSize.x, PaneSize.y);
        GUI.Box(rect,"Someshit");
		// Rect rect2 = new Rect(0f, rect.y - 30f, (float)UI.screenWidth - 85f, 30f);
        // GUI.Label(rect2,"AnotherShit");
        Vector2 vector = TitleSize;
        if (vector.x > (float)UI.screenWidth)
        {
            vector *= (float)UI.screenWidth / vector.x;
        }
        vector *= 0.5f;
        GUI.DrawTexture(new Rect(rect.x-40,rect.y-120, vector.x, vector.y), TexTitle, ScaleMode.StretchToFill, true);
        GUI.color = Color.white;
		rect.yMin += 17f;
        GUI.BeginGroup(rect);
        Rect rect2 = new Rect(30f, 0f, 170f, rect.height);
		Rect rect3 = new Rect(rect2.xMax + 17f, 0f, 145f, rect.height);
        GUI.Box(rect2,"");
        
        
        

        float num = 0f;
            GUI.BeginGroup(rect2);
        for(int i = 0 ; i < eventCall.Count; i++)
        {
            Vector2 vec = new Vector2(0,num);
            num += 50;
            Rect rectButton = new Rect(vec.x, vec.y, rect2.width, 50);
            //GUI.Label(rectButton,"Button");
            if(eventCall.ContainsKey(i))
            {
                if(GUI.Button(rectButton,eventCall[i].name))
                {
                    eventCall[i].action?.Invoke();
                }
            }
             
        }
            GUI.EndGroup();

        GUI.Box(rect3,"");
            GUI.BeginGroup(rect3);
            GUI.EndGroup();

        GUI.EndGroup();
    }

    private static Dictionary<int,(string name ,Action action)> eventCall = new()
    {
        {
            0,
            ("New Map",delegate()
            {
                Current.SceneEntry.uiRoot.windowUI.Add(new Page_NewMap());
            })
        },
        {
            1,
            ("Load Map",delegate()
            {
                Debug.Log(1);
            })
        },
        {
            2,
            ("Multiplayer",delegate()
            {
                Debug.Log(2);
            })
        },
        {
            3,
            ("Workshop",delegate()
            {
                Debug.Log(3);
            })
        },
        {
            4,
            ("Settings",delegate()
            {
                Debug.Log(4);
            })
        },
        {
            5,
            ("Exit",delegate()
            {
                Debug.Log(5);
            })
        }
        
    };

    private static readonly Vector2 PaneSize = new Vector2(450f, 550f);
    private static readonly Vector2 TitleSize = new Vector2(1024f, 145f);
    private static readonly Texture2D TexTitle = FindContent<Texture2D>.Get("GameTitle.png", true,true);

    private static readonly Texture2D BackGround = FindContent<Texture2D>.Get("background.png", true,true);
}
