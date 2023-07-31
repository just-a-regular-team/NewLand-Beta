
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePlaying : OnScene
{
    // Start is called before the first frame update
    public override void Start()
    {
        UI = new UIPlaying();
        base.Start();
        UI.UI_Init();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnGUI()
    {
        base.OnGUI();
    }
}
