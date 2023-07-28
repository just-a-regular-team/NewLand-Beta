using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputData
{
    public KeyCode mainkey;
    public KeyCode quickKey;
    public EventType typeEvent;
    public delegate void InputEvent();
    public InputEvent inputEvent;
    public InputData(KeyCode key,EventType typeE,InputEvent input)
    {
        mainkey = key;
        typeEvent = typeE;
        inputEvent += input;

        KeyInputData.Add(this);
    }


    public bool GetKeyDown
	{
		get
		{
			return KeyInputData.Contains(this) && (Input.GetKeyDown(mainkey) || Input.GetKeyDown(quickKey) && typeEvent == EventType.KeyDown);
		}
	} 
	 
 
	public bool JustPressed
	{
		get
		{
			return KeyInputData.Contains(this) && (Input.GetKey(mainkey) || Input.GetKey(quickKey));
		}
	}
 
	public bool GetKeyUp
	{
		get
		{
			 
			return KeyInputData.Contains(this) && (Input.GetKeyUp(mainkey) || Input.GetKeyUp(quickKey)) && typeEvent == EventType.KeyUp;
		}
	}

    public static List<InputData> KeyInputData = new List<InputData>();
}
