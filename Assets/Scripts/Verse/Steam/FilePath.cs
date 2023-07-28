
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FilePath
{
    public static string ContentPath<T>()
		{
			if (typeof(T) == typeof(AudioClip))
			{
				return "Audio/";
			}
			if (typeof(T) == typeof(Texture2D))
			{
				return "Texture/";
			}
			if (typeof(T) == typeof(string))
			{
				return "Strings/";
			}
			throw new ArgumentException();
		}
	private static string GetOrCreateModsFolder(string folderName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);//check if in editor the folder will be create at [Unityproject]/Assets
			DirectoryInfo directoryInfo2;//main dir 
			if (Application.isEditor)
			{
				directoryInfo2 = directoryInfo;
			}
			else
			{
				directoryInfo2 = directoryInfo.Parent;
			}
			string text = Path.Combine(directoryInfo2.ToString(), folderName);
			DirectoryInfo directoryInfo3 = new DirectoryInfo(text);
			if (!directoryInfo3.Exists)
			{
				directoryInfo3.Create();
				Debug.LogWarning($"Foulder {folderName} was at path:{directoryInfo3}");
			}
			return text;
		}
	public static string ModsFolderPath
		{
			get
			{
				if (FilePath.modsFolderPath == null)
				{
					FilePath.modsFolderPath = FilePath.GetOrCreateModsFolder("Mods");
				}
				return FilePath.modsFolderPath;
			}
		}	


	private static string modsFolderPath = null;
}
