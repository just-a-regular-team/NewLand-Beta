using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
     
    public TimeController GetTimeController
    {
        get
        {
            return _timeController;
        }
    }


    private static TimeController _timeController = new TimeController();

    public void UpdateGamePlay()
    {
        _timeController.TickControllerUpdate();
    }
}
