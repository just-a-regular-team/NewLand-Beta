using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinding
{
    public KeyCode mainkey;
    public KeyCode quickKey;
    public EventType typeEvent;
    public delegate void InputEvent();
    public InputEvent inputEvent;
    public KeyBinding(KeyCode key,EventType typeE,InputEvent input)
    {
        mainkey = key;
        typeEvent = typeE;
        inputEvent += input;

        keyBindings.Add(this);
    }


    public bool GetKeyDown
	{
		get
		{
			return keyBindings.Contains(this) && (Input.GetKeyDown(mainkey) || Input.GetKeyDown(quickKey) && typeEvent == EventType.KeyDown);
		}
	} 
	 
 
	public bool JustPressed
	{
		get
		{
			return keyBindings.Contains(this) && (Input.GetKey(mainkey) || Input.GetKey(quickKey));
		}
	}
 
	public bool GetKeyUp
	{
		get
		{
			 
			return keyBindings.Contains(this) && (Input.GetKeyUp(mainkey) || Input.GetKeyUp(quickKey)) && typeEvent == EventType.KeyUp;
		}
	}

    public static List<KeyBinding> keyBindings = new List<KeyBinding>();
}
