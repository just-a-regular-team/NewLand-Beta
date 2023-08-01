using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntry : OnScene
{
    // Start is called before the first frame update
    public override void Start()
    {
        UI = new UIEntry();
        base.Start();
        UI.UI_Init();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        UI.UI_Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnGUI()
    {
        base.OnGUI();
        UI.UI_OnGUI();
    }
}
