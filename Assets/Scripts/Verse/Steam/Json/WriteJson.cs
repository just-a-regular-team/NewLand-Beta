using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static class WriteJson
{
    
    public static void CreatAndWrite(List<ModData> ModPack)
    {
        
        string json = JsonConvert.SerializeObject(ModPack,Formatting.Indented);
        string pathfile = FilePath.ModsFolderPath+"/Config.json";
        if (File.Exists(pathfile))
        {
        File.Delete(pathfile);
        using (var st = new StreamWriter(pathfile, true))
        {
	    st.WriteLine(json.ToString());
	    st.Close();
        }
        }else if (!File.Exists(pathfile)) 
        {
        using (var st = new StreamWriter(pathfile, true))
        {
	        st.WriteLine(json.ToString());
	        st.Close();
        }
        }       
         
        
       
        
         
    }
}
