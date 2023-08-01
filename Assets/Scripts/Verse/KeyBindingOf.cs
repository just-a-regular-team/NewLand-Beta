using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyBindingOf
{
    public static void InitKeyBinding(){}//Auto init all shit here
    static KeyBindingOf(){}
    public static KeyBinding Accept = new KeyBinding(KeyCode.Return,EventType.KeyDown,null);
    public static KeyBinding Cancel = new KeyBinding(KeyCode.Escape,EventType.KeyDown,null);
    public static KeyBinding ChangeToBuildMode = new KeyBinding(KeyCode.B,EventType.KeyDown,delegate {Mouse.GetMouse.ChangeModeTo(Mouse.MouseMode.Build);});
    public static KeyBinding ChangeToViewStragety = new KeyBinding(KeyCode.V,EventType.KeyDown,delegate {Mouse.GetMouse.ChangeModeTo(Mouse.MouseMode.ViewStragety);});
    public static KeyBinding TimeFast =  new KeyBinding(KeyCode.M,EventType.KeyDown,delegate {Debug.LogWarning("Change time to fast");Current.GetGamePlaying.GetTimeController.ChangeTimeSpeed = TimeSpeed.Fast;});
    public static KeyBinding TimeNormal = new KeyBinding(KeyCode.N,EventType.KeyDown,delegate {Debug.LogWarning("Change time to normal");Current.GetGamePlaying.GetTimeController.ChangeTimeSpeed = TimeSpeed.Normal;});
}
