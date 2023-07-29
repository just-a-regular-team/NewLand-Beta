using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeController
{
    public TimeSpeed ChangeTimeSpeed
    {
        set { curTimeSpeed = value;}
    }

    private TimeSpeed curTimeSpeed = TimeSpeed.Normal;
    private int ticksGameInt;
    private int tickInFrame;
    private float realTimeToTickThrough;
    
    private TickList TickNormal = new TickList(TickType.Normal);
    private TickList TickRare = new TickList(TickType.Long);
    private TickList TickLong = new TickList(TickType.Long);
    private Stopwatch clock = new Stopwatch();
    private float TimeMultiplier
    {
        get
        {
            switch(curTimeSpeed)
            {
                case TimeSpeed.Normal:
                return 1;
                case TimeSpeed.Fast:
                return 3;
                case TimeSpeed.SuperFast:
                return 6;
                default:
                return -1;

            }
        }
    }
    private float CurTimePerTick
    {
        get
        {
            if(TimeMultiplier == 0) return 0;
            return 1/(60f * TimeMultiplier);
        }
    }
    public void TickControllerUpdate()
    {
        tickInFrame = 0;
        if(curTimeSpeed != TimeSpeed.Pause) // Or not open UI like menu or something like that
        {
            float curTimePerTick = CurTimePerTick;
            if (Mathf.Abs(Time.deltaTime - curTimePerTick) < curTimePerTick * 0.1f)
			{
			    this.realTimeToTickThrough += curTimePerTick;
			}
			else
			{
				this.realTimeToTickThrough += Time.deltaTime;
			}
            clock.Reset();
            clock.Start();
            while (this.realTimeToTickThrough > 0f && (float)this.tickInFrame < TimeMultiplier * 2f)
            {
                DoSingleTick();
                tickInFrame++;
                if(curTimeSpeed == TimeSpeed.Pause && (float)this.clock.ElapsedMilliseconds > 45.454544f)
                {
                    break;
                }
            }
            if(realTimeToTickThrough > 0 ){ realTimeToTickThrough = 0;}
        }
    }

    private void DoSingleTick()
    {
        //Map tick update first
        ticksGameInt++;

        TickNormal.DoTick();
        TickRare.DoTick();
        TickLong.DoTick();

        //Another update tick in here

    }
}

public enum TimeSpeed
{
    Pause,
    Normal,
    Fast,
    SuperFast
}
