using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResolutionUtility  
{
    public static Resolution NativeResolution
    {
        get
        {
            Resolution[] resolutions = Screen.resolutions;
            if (resolutions.Length == 0)
            {
                return Screen.currentResolution;
            }
            Resolution result = resolutions[0];
            for (int i = 1; i < resolutions.Length; i++)
            {
                if (resolutions[i].width > result.width || (resolutions[i].width == result.width && resolutions[i].height > result.height))
                {
                    result = resolutions[i];
                }
            }
            return result;
        }
    }
    public static float GetRecommendedUIScale(int screenWidth, int screenHeight)
    {
        // if (screenWidth == 0 || screenHeight == 0)
        // {
        //     Resolution nativeResolution = ResolutionUtility.NativeResolution;
        //     screenWidth = nativeResolution.width;
        //     screenHeight = nativeResolution.height;
        // }
        if (screenWidth <= 1024 || screenHeight <= 768)
        {
            return 1f;
        }
        
        return 1f;
    }
    public static bool BorderlessFullscreen
    {
        get
        {
            if (ResolutionUtility.borderlessFullscreenCached == null)
            {
                ResolutionUtility.borderlessFullscreenCached = new bool?(System.Environment.GetCommandLineArgs().Contains("-popupwindow"));
            }
            return ResolutionUtility.borderlessFullscreenCached.Value;
        }
    }
    public static void SetResolutionRaw(int w, int h, bool fullScreen)
		{
        if (Application.isBatchMode)
        {
            return;
        }
        if (w <= 0 || h <= 0)
        {
            Debug.LogError(string.Concat(new object[]
            {
                "Tried to set resolution to ",
                w,
                "x",
                h
            }));
            return;
        }
        if (Screen.width != w || Screen.height != h || Screen.fullScreen != fullScreen)
        {
            Screen.SetResolution(w, h, fullScreen);
        }
    }

    public static void SetNativeResolutionRaw()
    {
        Resolution nativeResolution = ResolutionUtility.NativeResolution;
        ResolutionUtility.SetResolutionRaw(nativeResolution.width, nativeResolution.height, !ResolutionUtility.BorderlessFullscreen);
    }
    private static bool? borderlessFullscreenCached;
}
