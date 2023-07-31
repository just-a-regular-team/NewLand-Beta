using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScene : MonoBehaviour
{
     Map map;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
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
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.M)))
        {
            Debug.LogWarning("Change time to fast");
            Current.GetGamePlaying.GetTimeController.ChangeTimeSpeed = TimeSpeed.Fast;
        }else if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.LogWarning("Change time to normal");
            Current.GetGamePlaying.GetTimeController.ChangeTimeSpeed = TimeSpeed.Normal;
        }
        Current.GetGamePlaying.UpdateGamePlay();
        map.MapUpdate();

        player.UpdatePlayer();
    }

    public void FixedUpdate()
    {
        player.FixedUpdatePlayer();
    }

    public void OnGUI()
    {
        player.UpdatePlayerOnGUI();
    }
}
