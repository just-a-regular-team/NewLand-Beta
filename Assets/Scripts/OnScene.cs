using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScene : MonoBehaviour
{
     

    // Start is called before the first frame update
    void Start()
    {
        ModLoad.LoadingMod();
        Controller.ResetAll();
         
        Map map = new Map("map_test",5);
        map.CreateNewMap();
        Current.SetGame = new Game();//just leave it temporarily here
         
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
    }
}
