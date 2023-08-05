
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


        map = new Map("map_test",10);
        map = map.CreateNewMap();
        
        
        player = new Player("Player");
        player.RegisterPlayerToWorld();

        DataOfPawn dataOfPawn = new DataOfPawn()
        {
            tickType = TickType.Normal,
            GraphicData = new GraphicData()
            {
                graphic = typeof(SingleGraphic)
            }
        };
        Vector2 pos = Vector2.zero;
        Pawn pawn = new Pawn()
        {
            name = "Pawn",
            position = pos,
            data = dataOfPawn
        };

        pawn.SpawnSetup(Map.MapWorking);
        Material mat = DefaultContent.BadMat;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        player.UpdatePlayer();

        uiRoot.UI_Update();
        Current.GetGamePlaying.UpdateGamePlay();
        map.MapUpdate();
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
