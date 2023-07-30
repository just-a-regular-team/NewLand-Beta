using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controller
{
    static SpriteManager sp;
     
    public static void ResetAll()
    {
        sp = new SpriteManager();
        Current.Notify_LoadedSceneChanged();
    }

    public static SpriteManager SpriteManager {get{return sp;}}
}
