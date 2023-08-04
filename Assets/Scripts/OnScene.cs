using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnScene : MonoBehaviour
{
    protected bool Transitions;
    public UIRoot uiRoot;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        KeyBindingOf.InitKeyBinding();
        Settings.CreateOrLoad();
        Current.Notify_LoadedSceneChanged();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Transitions = false;
        ResolutionUtility.Update();
		TimeProperty.Update();
        LongEvent.UpdateLongEvent(out bool CheckGotoAntherScene);
        if(CheckGotoAntherScene)
        {
            Transitions = true;
        }

    }

    public virtual void FixedUpdate()
    {
         
    }

    public virtual void OnGUI()
    {
        if(Transitions){return;} // stop all UI to Transitions
        GUI.depth = 50;
        UI.ApplyUIScale();

        // fadeTexture = new Texture2D(1, 1)
        // {
        //     name = "FadeTex",
        // };
        // backgroundStyle.normal.background = fadeTexture;
        // fadeTexture.SetPixel(0, 0, Color.white);
		// fadeTexture.Apply();
        // GUI.Label(new Rect(-10f, -10f, UI.screenWidth + 10f, UI.screenHeight + 10f), fadeTexture, backgroundStyle);
    }
    // private GUIStyle backgroundStyle = new();
	// private Texture2D fadeTexture;
}
