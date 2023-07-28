using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class ModLoad
{
    private static List<ModData> ModsInFoulder = new List<ModData>();
    private static List<ModData> ModIsEnable = new List<ModData>();
    public static void LoadingMod()
    {
        ResetListMod();
        SaveConfigMods();
        
    }

    public static void ResetListMod()
    {
        ModsInFoulder.Clear();
        foreach(string localPath in from d in new DirectoryInfo(FilePath.ModsFolderPath).GetDirectories() // find all mod foulder at dir 
			select d.FullName)
        {
            ModData mod = new ModData(localPath);
            ModsInFoulder.Add(mod);
        }
    }
    private static void SaveConfigMods()
    {   
         
        // List<ModData> modsEnable = new List<ModData>();
        // foreach(ModData mod in ModsInFoulder)
        // {
        //     if(mod.Enable)
        //     {
        //         numLoad++;
        //         mod.numLoadOrder=numLoad; 
        //         modsEnable.Add(mod);
        //     }
        // }
        int numLoad = 0;
        //Warning
        //what is this piece of shit? so fucking dumb asshole. U need to fucking try new thing!!!It so fucking random
        for(int i = 0; i < ModsInFoulder.Count;i++)
        {
            ModData mod = ModsInFoulder[i];
            if(mod.Enable)
            {
                numLoad++;
                mod.numLoadOrder=numLoad;
                ModIsEnable.Add(mod);
            }
        }
        WriteJson.CreatAndWrite(ModIsEnable.ToList());
    }
    
}
