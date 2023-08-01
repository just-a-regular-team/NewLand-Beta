using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI
{
    public static void ApplyUIScale()
    {
        if (Settings.UIScale == 1f)
        {
            UI.screenWidth = Screen.width;
            UI.screenHeight = Screen.height;
            return;
        }
        UI.screenWidth = Mathf.RoundToInt((float)Screen.width / Settings.UIScale);
        UI.screenHeight = Mathf.RoundToInt((float)Screen.height / Settings.UIScale);
        float uiscale = Settings.UIScale;
        float uiscale2 = Settings.UIScale;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0f, 0f, 0f), Quaternion.identity, new Vector3(uiscale, uiscale2, 1f));
    }
    public static int screenWidth;
    public static int screenHeight;
}
