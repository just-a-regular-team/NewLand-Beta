using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData
{
    #region volume
    public float volumeMusic;
    public float volumeSound;
    public float volumeAmbient;
    #endregion

    #region video
    public int screenWidth; 
	public int screenHeight;
	public bool fullscreen;
    #endregion

    #region general
    public float uiScale = 1f;
    public float ZoomInViewStragety;
    public float minZoomInViewStragety;
    #endregion

    #region cursor
    public bool customCursorEnabled = false;
    #endregion
    public void Apply()
    {
        
        if (this.customCursorEnabled)
        {
            CustomCursor.Activate();
        }
        else
        {
            CustomCursor.Deactivate();
        }
        if (this.screenWidth == 0 || this.screenHeight == 0)
        {
            ResolutionUtility.SetNativeResolutionRaw();
            return;
        }
        ResolutionUtility.SetResolutionRaw(this.screenWidth, this.screenHeight, !ResolutionUtility.BorderlessFullscreen && this.fullscreen);
    }
}
