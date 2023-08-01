
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FilePath
{
	public static bool TryGetCommandLineArg(string key, out string value)
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i].Contains('='))
			{
				string[] array = commandLineArgs[i].Split(new char[]
				{
					'='
				});
				if (array.Length == 2 && (string.Compare(array[0], key, true) == 0 || string.Compare(array[0], "-" + key, true) == 0))
				{
					value = array[1];
					return true;
				}
			}
		}
		value = null;
		return false;
	}
	public static string SaveDataFolderPath
    {
        get
        {
            if (saveDataPath == null)
            {

                if (TryGetCommandLineArg("savedatafolder", out var value))
                {
                    value.TrimEnd('\\', '/');
                    if (value == "")
                    {
                        value = Path.DirectorySeparatorChar.ToString() ?? "";
                    }
                    saveDataPath = value;
                    Debug.Log("Save data folder overridden to " + saveDataPath);
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
                    if (Application.isEditor)
                    {
                        saveDataPath = Path.Combine(directoryInfo.ToString(), "Data");
                    }
                    else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
                    {
                        string path = Path.Combine(Directory.GetParent(Application.persistentDataPath).ToString(), "NewLand");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        saveDataPath = path;
                    }
                    else
                    {
                        saveDataPath = Application.persistentDataPath;
                    }
                }
                DirectoryInfo directoryInfo2 = new DirectoryInfo(saveDataPath);
                if (!directoryInfo2.Exists)
                {
                    directoryInfo2.Create();
                }
            }
            Debug.Log(saveDataPath);
            return saveDataPath;
        }
    }
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
	private static string FolderUnderSaveData(string folderName)
	{
		string text = Path.Combine(FilePath.SaveDataFolderPath, folderName);
		DirectoryInfo directoryInfo = new DirectoryInfo(text);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
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
	public static string ConfigFolderPath => FolderUnderSaveData("SaveData/Config");
    public static string SavedGamesFolderPath => FolderUnderSaveData("SaveData/Saves");
	public static string PrefsFilePath
	{
		get
		{
			return Path.Combine(ConfigFolderPath, "SettingConfig.json");
		}
	}
	private static string saveDataPath = null;
    private static string modsFolderPath = null;
}
