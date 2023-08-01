
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
        UI = new UIPlaying();
        base.Start();
        UI.UI_Init();


        map = new Map("map_test",5);
        map.CreateNewMap();
        Current.SetGame = new Game();//just leave it temporarily here
        
        player = new Player("Player");
        player.RegisterPlayerToWorld();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        map.MapUpdate();
        player.UpdatePlayer();

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
        player.UpdatePlayerOnGUI();
    }
}
