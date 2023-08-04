using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Current
{
    static Game game;
    static Camera mainCamera;
    private static OnScene sceneRoot;
    static ScenePlaying scenePlaying;
    static SceneEntry sceneEntry;
     
    public static void Notify_LoadedSceneChanged()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        {
            if (InEntryScene)
			{
				sceneEntry = GameObject.Find("OnScene").GetComponent<SceneEntry>();
				Current.scenePlaying = null;
				Current.sceneRoot = sceneEntry;
				return;
			}
			if (InPlayScene)
			{
                CameraFollow = mainCamera.gameObject.AddComponent<CameraFollow>();
				Current.sceneEntry = null;
				Current.scenePlaying = GameObject.Find("OnScene").GetComponent<ScenePlaying>();
				Current.sceneRoot = Current.scenePlaying;
			}
        }
    }


    public static Game GetGamePlaying {get{return game;}}
    public static Game SetGame{set{game = value;}}

    public static Camera Camera{get{return mainCamera;}}
    public static CameraFollow CameraFollow;

    public static OnScene SceneRoot{get{return sceneRoot;}}
    public static OnScene ScenePlaying{get{return scenePlaying;}}
    public static OnScene SceneEntry{get{return sceneEntry;}}

    public static bool InEntryScene
    {
        get
        {
            return SceneManager.GetActiveScene().name == "Entry";
        }
    }
    public static bool InPlayScene
    {
        get
        {
            return SceneManager.GetActiveScene().name == "Play";
        }
    }
    public static string GetSceneName
    {
        get
        {
            return SceneManager.GetActiveScene().name;
        }
    }
    
}
