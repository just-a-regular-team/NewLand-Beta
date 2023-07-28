using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ContentAssets<T> where T:class
{
    private static string[] AcceptableExtensionsAudio = new string[]
	{
		".wav",
		".mp3",
		".ogg",
		".xm",
		".it",
	    ".mod",
		".s3m"
	};
	private static string[] AcceptableExtensionsTexture = new string[]
	{
		".png",
		".jpg",
		".jpeg",
		".psd"
	};
	private static string[] AcceptableExtensionsString = new string[]
	{
		".txt"
	};


    public static bool IsAcceptableExtension(string extension)
	{
		string[] array;
		if (typeof(T) == typeof(AudioClip))
		{
			array = ContentAssets<T>.AcceptableExtensionsAudio;
		}
		else if (typeof(T) == typeof(Texture2D))
		{
			array = ContentAssets<T>.AcceptableExtensionsTexture;
		}
		else
		{
			if (!(typeof(T) == typeof(string)))
			{
				Debug.LogError("Unknown content type " + typeof(T));
				return false;
			}
			array = ContentAssets<T>.AcceptableExtensionsString;
		}
		foreach (string b in array)
		{
			if (extension.ToLower() == b)
			{
				return true;
			}
		}
		return false;
	}

	 

    public static Texture2D LoadTexture(FileInfo file)
	{
		Texture2D texture2D = null;
		if (file.Exists)
		{
			byte[] data = File.ReadAllBytes(file.FullName);
			texture2D = new Texture2D(2, 2, TextureFormat.Alpha8, true);
			texture2D.LoadImage(data);
			texture2D.name = Path.GetFileNameWithoutExtension(file.Name);
			texture2D.filterMode = FilterMode.Trilinear;
			texture2D.anisoLevel = 2;
			texture2D.Apply(true, true);
		}
		return texture2D;
	}
}
