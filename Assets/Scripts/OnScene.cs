using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnScene : MonoBehaviour
{
    public UIRoot UI;
     Map map;
    Player player;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Settings.CreateOrLoad();
        Current.Notify_LoadedSceneChanged();
        ModLoad.LoadingMod();
        Controller.ResetAll();
        foreach(ModData modData in ModLoad.ModsInFoulder)
        {
            ReadXml xml = new ReadXml();
            xml.ReadAllXml(modData.rootDirInt);
        }
         
        map = new Map("map_test",5);
        map.CreateNewMap();
        Current.SetGame = new Game();//just leave it temporarily here
        
        player = new Player("Player");
        player.RegisterPlayerToWorld();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        ResolutionUtility.Update();
		TimeProperty.Update();
        
        Current.GetGamePlaying.UpdateGamePlay();
        map.MapUpdate();

        player.UpdatePlayer();
    }

    public virtual void FixedUpdate()
    {
        player.FixedUpdatePlayer();
    }

    public virtual void OnGUI()
    {
        player.UpdatePlayerOnGUI();
    }
}
