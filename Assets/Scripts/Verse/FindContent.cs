using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FindContent<T> where T : class
{
	public static Dictionary<string, (ModData modData,T t)> ListAllContent = new Dictionary<string,(ModData modData,T t)>();
    public static T Get(string itemPath,bool reportFailure = true,bool moreLoadIfFalse = false)
    {
    //    if (!UnityData.IsInMainThread)
	// 	{
	// 		Log.Error("Tried to get a resource \"" + itemPath + "\" from a different thread. All resources must be loaded in the main thread.");
	// 		return default(T);
	// 	}
		T t = default(T);

        if (typeof(T) == typeof(Texture2D))
		{
			t = (T)((object)Resources.Load<Texture2D>(FilePath.ContentPath<Texture2D>() + itemPath));
		}
		if (typeof(T) == typeof(AudioClip))
		{
			t = (T)((object)Resources.Load<AudioClip>(FilePath.ContentPath<AudioClip>() + itemPath));
		}
		if (t != null)
		{
			return t;
		}
		if(!moreLoadIfFalse)
		{
			goto theEnd;
		}
		t = ListAllContent[itemPath].t; //bruh just check list 

		if (t != null)
		{
			return t;
		}
		
		//Add more solution . Bcs idk what to do in the first time
		 
		theEnd:

        if (reportFailure)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Could not load ",
				typeof(T),
				" at ",
				itemPath,
				" in any active mod or in base resources."
			}));
		}
		return default(T);
    }
}
