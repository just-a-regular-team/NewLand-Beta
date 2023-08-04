
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
	public static void ResetShutDown()
	{
		Singleton<T>.m_ShuttingDown = false;
	} 
	public static T Instance
	{
		get
		{
			T result;
			if (Singleton<T>.m_ShuttingDown)
			{
				string str = "[Singleton] Instance '";
				Type typeFromHandle = typeof(T);
				Debug.LogWarning(str + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + "' already destroyed. Returning null.");
				result = default(T);
				return result;
			}
			object @lock = Singleton<T>.m_Lock;
			lock (@lock)
			{
				if (Singleton<T>.m_Instance == null)
				{
					Singleton<T>.m_Instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
					if (Singleton<T>.m_Instance == null)
					{
						GameObject gameObject = new GameObject();
						//Singleton<T>.m_Instance = gameObject.AddComponent<T>();
						gameObject.name = $"Singleton<{typeof(T).Name}>";

						GameObject parent = GameObject.Find("Singleton");
						if(parent != null)
						{
							gameObject.transform.SetParent(parent.transform);
						}else
						{
							parent = new GameObject("Singleton");
							gameObject.transform.SetParent(parent.transform);
						}
						 
					}
				}
				result = Singleton<T>.m_Instance;
			}
			return result;
		}
	} 
	private void OnApplicationQuit()
	{
		Singleton<T>.m_ShuttingDown = true;
	} 
	private void OnDestroy()
	{
		Singleton<T>.m_ShuttingDown = true;
	} 
	private static bool m_ShuttingDown = false; 
	private static object m_Lock = new object(); 
	private static T m_Instance;
}
