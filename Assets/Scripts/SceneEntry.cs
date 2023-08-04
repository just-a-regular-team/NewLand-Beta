using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntry : OnScene
{
    // Start is called before the first frame update
    public override void Start()
    {
        uiRoot = new UIEntry();
        base.Start();
        ModLoad.LoadingMod();
        Controller.ResetAll();
        foreach(ModData modData in ModLoad.ModsInFoulder)
        {
            ReadXml xml = new ReadXml();
            xml.ReadAllXml(modData.rootDirInt);
        }
        uiRoot.UI_Init();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        uiRoot.UI_Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnGUI()
    {
        base.OnGUI();
        uiRoot.UI_OnGUI();
    }
}
