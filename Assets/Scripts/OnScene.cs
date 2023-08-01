using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnScene : MonoBehaviour
{
    public UIRoot UI;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        KeyBindingOf.InitKeyBinding();
        Settings.CreateOrLoad();
        Current.Notify_LoadedSceneChanged();
        ModLoad.LoadingMod();
        Controller.ResetAll();
        foreach(ModData modData in ModLoad.ModsInFoulder)
        {
            ReadXml xml = new ReadXml();
            xml.ReadAllXml(modData.rootDirInt);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        ResolutionUtility.Update();
		TimeProperty.Update();
    
    }

    public virtual void FixedUpdate()
    {
         
    }

    public virtual void OnGUI()
    {
        
    }
}
