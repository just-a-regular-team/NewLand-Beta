using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Settings
{
    public static int ScreenWidth
    {
        get
        {
            return data.screenWidth;
        }
        set
        {
            data.screenWidth = value;
        }
    }
    public static int ScreenHeight
    {
        get
        {
            return data.screenHeight;
        }
        set
        {
            data.screenHeight = value;
        }
    }
    public static float UIScale
    {
        get
        {
            return data.uiScale;
        }
        set
        {
            data.uiScale = value;
        }
    }


    public static void CreateOrLoad()
    {
        bool flag = !new FileInfo(FilePath.PrefsFilePath).Exists;
        data = new SettingData();
        if (flag)
        {
            data.uiScale = ResolutionUtility.GetRecommendedUIScale(data.screenWidth, data.screenHeight);
            //data.langFolderName = LanguageDatabase.SystemLanguageFolderName();
        }else
        {
            data = JsonConvert.DeserializeObject<SettingData>(File.ReadAllText(FilePath.PrefsFilePath));
        }
        Apply();
    }
    public static void Save()
    {
        string json = JsonConvert.SerializeObject(data,Formatting.Indented);
        if (File.Exists(FilePath.PrefsFilePath))
        {
        File.Delete(FilePath.PrefsFilePath);
        using (var st = new StreamWriter(FilePath.PrefsFilePath, true))
        {
	    st.Write(json.ToString());
	    st.Close();
        }
        }else if (!File.Exists(FilePath.PrefsFilePath)) 
        {
        using (var st = new StreamWriter(FilePath.PrefsFilePath, true))
        {
	        st.Write(json.ToString());
	        st.Close();
        }
        }       
    }
    public static void Apply()
	{
		data.Apply();
        Save();
	}
    static SettingData data;
}
