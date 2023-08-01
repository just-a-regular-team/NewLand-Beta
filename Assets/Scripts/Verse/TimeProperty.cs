using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class TimeProperty
{
    public static void Update()
    {
        time = Time.time;
        frameCount = Time.frameCount;
        deltaTime = Time.deltaTime;
        float realtimeSinceStartup = Time.realtimeSinceStartup;
        realDeltaTime = realtimeSinceStartup - lastRealTime;
        lastRealTime = realtimeSinceStartup;
        // if (Current.ProgramState == ProgramState.Playing)
        // {
            if (Current.GetGamePlaying != null)
            {
                unpausedTime += deltaTime * Current.GetGamePlaying.GetTimeController.TimeMultiplier;
            }
        //}
        if (DebugSettings.lowFPS && Time.deltaTime < 100f)
        {
            Thread.Sleep((int)(100f - Time.deltaTime));
        }
    }
    public static float time;
    public static float deltaTime;
    public static float realDeltaTime;
    public static int frameCount;
    private static float unpausedTime;
    private static float lastRealTime = 0f;
}
