using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI
{
    public static Vector2 MousePositionOnUIInverted
    {
        get
        {
            Vector2 mousePositionOnUI = (Input.mousePosition / Settings.UIScale);
            mousePositionOnUI.y = (float)UI.screenHeight - mousePositionOnUI.y;
            return mousePositionOnUI;
        }
    }
    public static Vector2 MousePosUIInvertedUseEventIfCan
    {
        get
        {
            if (Event.current != null)
            {
                return UI.GUIToScreenPoint(Event.current.mousePosition);
            }
            return UI.MousePositionOnUIInverted;
        }
    }
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
    public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIUtility.GUIToScreenPoint(guiPoint / Settings.UIScale);
		}
    public static int screenWidth;
    public static int screenHeight;
}
