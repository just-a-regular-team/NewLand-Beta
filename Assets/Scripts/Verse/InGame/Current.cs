using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Current
{
    static Game game;
    static Camera mainCamera;



    public static void Notify_LoadedSceneChanged()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        CameraFollow = mainCamera.gameObject.AddComponent<CameraFollow>();
    }


    public static Game GetGamePlaying {get{return game;}}
    public static Game SetGame{set{game = value;}}
    public static Camera Camera{get{return mainCamera;}}
    public static CameraFollow CameraFollow;
}
