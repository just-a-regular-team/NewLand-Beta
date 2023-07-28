using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModContent<T> where T : class
{

    public ModContent(ModData mod)
    {
        this.mod = mod;
    }
    public void ClearDestroy()
		{
			// if (typeof(UnityEngine.Object).IsAssignableFrom(typeof(T)))
			// {
			// 	foreach (T localObj2 in this._ContentHolder.Values)
			// 	{
			// 		T localObj = localObj2;
			// 		// LongEventHandler.ExecuteWhenFinished(delegate
			// 		// {
			// 		// 	UnityEngine.Object.Destroy((UnityEngine.Object)((object)localObj));
			// 		// });
			// 	}
			// }
			for (int i = 0; i < this.Disposables.Count; i++)
			{
				this.Disposables[i].Dispose();
			}
			this.Disposables.Clear();
			this._ContentHolder.Clear();
			
	}
    public void ReloadAll(DirectoryInfo dir,Func<string, bool> validateExtension = null)//Remember you not add folder version yet. Like path = dir.FullName + [FolderVersionSupport] + "/Content" + ...";
    {
        string path;
        if(typeof(T)==typeof(Texture2D))
        {
            path = dir.FullName + "/Texture";
        }else if(typeof(T)==typeof(AudioClip))
        {
            path = dir.FullName + "/Audio/";
        }else if(typeof(T)==typeof(string))
        {
            path = dir.FullName + "/Strings/";
        }else
        {
            path = dir.FullName + "/Content/"+typeof(T).Name;
        }

        Dictionary<string, FileInfo> allFileContentForMod = new Dictionary<string, FileInfo>();
        Dictionary<string, T> result = new Dictionary<string, T>();
        if(Directory.Exists(path))
        { 
            //was Solution: (this is bad idea in here: the key is different another key in the diffenrent mod but maybe it be the same key in allMod)
            //(What happen when we have the same key in diffent mod how we gonna take value from it?) 
            foreach(FileInfo content in new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories))
            {
                
                if(validateExtension == null || validateExtension(content.Extension))
                {
                   string key = content.FullName.Substring(path.Length + 1);
                     
			    	if (!allFileContentForMod.ContainsKey(key))
			    	{
			    	    allFileContentForMod.Add(key, content);
                        T t = default(T);

                        try
                        {
                            if (typeof(T) == typeof(string))
				            {
					            t = (T)(object)File.ReadAllText(content.FullName);
				            }
				            if (typeof(T) == typeof(Texture2D))
				            {
					            t = (T)((object)ContentAssets<T>.LoadTexture(content));
				            }
                            //Waring: Audio????
                        }
                        catch (System.Exception ex)
                        {
                            Debug.LogError(string.Concat(new object[]
				            {
					        "Exception loading ",
					        typeof(T),
					        " from file.\nabsFilePath: ",
					        content.FullName,
					        "\nException: ",
					        ex.ToString()
				            }));
                        }
                        finally
                        {
                             
				            key = key.Replace('\\', '/');
                            result.Add(key,t);
                            if(!FindContent<T>.ListAllContent.ContainsKey(key))
                            {
                               FindContent<T>.ListAllContent.Add(key,(mod,t)); 
                            }else
                            {
                                string newkey = mod.Name+"/"+key;
                                Debug.LogWarning(string.Concat(new object[]
                                {
                                    "The key: [",
                                    key,
                                    "] from mod: ",
                                    mod.Name,
                                    " is already exits with ",
                                    $"typeof({typeof(T)}) from mod: {(FindContent<T>.ListAllContent[key]).modData.Name}",
                                    $"\n so key:[{key}] will be change to key:[{newkey}]"
                                }));
                                FindContent<T>.ListAllContent.Add(newkey,(mod,t)); 
                            }
                        }
			    	}
                }
            }
        }

        _ContentHolder = result;
    }
    private ModData mod;
    private Dictionary<string,T> _ContentHolder = new Dictionary<string, T>();
    public List<IDisposable> Disposables = new List<IDisposable>();

    public Dictionary<string,T> getContents {get{return _ContentHolder;}}
    public Dictionary<string,T> setContentList {set{_ContentHolder = value;}}
}
