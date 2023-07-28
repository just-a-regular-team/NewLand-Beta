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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
