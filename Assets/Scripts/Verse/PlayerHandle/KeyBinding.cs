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

        AddKeyBingding(this);
    }


    public bool GetKeyDown
	{
		get
		{
			bool result = keyBindings.Contains(this) && (Input.GetKeyDown(mainkey) || Input.GetKeyDown(quickKey) && typeEvent == EventType.KeyDown);
			if(result)
			{
				CallBack();
			}
			return result;
		}
	} 
	 
 
	public bool JustPressed
	{
		get
		{
			bool result = keyBindings.Contains(this) && (Input.GetKey(mainkey) || Input.GetKey(quickKey));
			if(result)
			{
				CallBack();
			}
			return result;
		}
	}
 
	public bool GetKeyUp
	{
		get
		{
			bool result = keyBindings.Contains(this) && (Input.GetKeyUp(mainkey) || Input.GetKeyUp(quickKey)) && typeEvent == EventType.KeyUp;
			if(result)
			{
				CallBack();
			}
			return result;
		}
	}

	private void CallBack()
	{
		inputEvent?.Invoke();
	}


	private static void AddKeyBingding(KeyBinding keyBinding)
	{
		try
		{
			if(keyBindings.Contains(keyBinding))
			{
				Debug.LogError("keybingdings already have "+ keyBinding);
				return;
			}
			foreach(KeyBinding key in keyBindings)
			{
				if(key.mainkey == keyBinding.mainkey)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"Keybing have coincide on key ",
						key.mainkey
					}));
				}else if(key.quickKey == keyBinding.quickKey && keyBinding.quickKey != KeyCode.None)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"Keybing have coincide on quickkey ",
						key.quickKey
					}));
				}
			}
			keyBindings.Add(keyBinding);
		}
		catch (System.Exception ex)
		{
			Debug.LogError(ex);
		}
	}

    public static List<KeyBinding> keyBindings = new List<KeyBinding>();
}
