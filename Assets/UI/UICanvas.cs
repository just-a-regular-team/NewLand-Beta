using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas
{
    static Canvas canvasInt;
    static CanvasScaler canvasScaler;
    static GraphicRaycaster graphicRaycaster;

    public void InitCanvas()
    {
        {
            //Init canvas gameGO
            GameObject canvasGo = new GameObject("Canvas");
            canvasInt = canvasGo.AddComponent<Canvas>();
            canvasScaler = canvasGo.AddComponent<CanvasScaler>();
            graphicRaycaster = canvasGo.AddComponent<GraphicRaycaster>();

            canvasInt.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        }
    }
}
