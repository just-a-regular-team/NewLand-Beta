using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controller
{
    static SpriteManager sp;
     
    public static void ResetAll()
    {
        sp = new SpriteManager();
        
    }

    public static SpriteManager SpriteManager {get{return sp;}}
}
