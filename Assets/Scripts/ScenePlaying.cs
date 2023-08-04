
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePlaying : OnScene
{
    Map map;
    Player player;
    // Start is called before the first frame update
    public override void Start()
    {
        uiRoot = new UIPlaying();
        base.Start();
        uiRoot.UI_Init();


        map = new Map("map_test",5);
        map.CreateNewMap();
        
        
        player = new Player("Player");
        player.RegisterPlayerToWorld();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        map.MapUpdate();
        player.UpdatePlayer();

        uiRoot.UI_Update();
        Current.GetGamePlaying.UpdateGamePlay();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.FixedUpdatePlayer();
    }
    public override void OnGUI()
    {
        base.OnGUI();
        uiRoot.UI_OnGUI();
        player.UpdatePlayerOnGUI();
    }
}
