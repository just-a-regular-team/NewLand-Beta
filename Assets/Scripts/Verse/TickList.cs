using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickList
{
    public TickList(TickType tickType)
	{
		this.tickType = tickType;
		for (int i = 0; i < this.TickInterval; i++)
		{
			this.allThinglist.Add(new List<Thing>());
		}
	}
    private int TickInterval
	{
		get
		{
            return this.tickType switch
            {
                TickType.Normal => 1,
                TickType.Rare => 250,
                TickType.Long => 2000,
                _ => -1,
            };
        }
	}

    public void RegisterThing(Thing t)
	{
		this.ThingsToRegister.Add(t);
	}
	public void DeregisterThing(Thing t)
	{
		this.ThingsToDeregister.Add(t);
	}
    public void DoTick()
    {
        for (int i = 0; i < this.ThingsToRegister.Count; i++)
		{
			this.BucketOf(this.ThingsToRegister[i]).Add(this.ThingsToRegister[i]);
		}
		this.ThingsToRegister.Clear();
		for (int j = 0; j < this.ThingsToDeregister.Count; j++)
		{
			this.BucketOf(this.ThingsToDeregister[j]).Remove(this.ThingsToDeregister[j]);
		}
		this.ThingsToDeregister.Clear();

		List<Thing> list = this.allThinglist[Current.GetGamePlaying.GetTimeController.ticksGameInt % this.TickInterval];
		for (int m = 0; m < list.Count; m++)
		{
			if (!list[m].Destroyed)
			{
				try
				{
					switch (this.tickType)
					{
					case TickType.Normal:
						list[m].Tick();
						break;
					case TickType.Rare:
						list[m].TickRare();
						break;
					case TickType.Long:
						list[m].TickLong();
						break;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}

			}
		}
    }

    public TickType tickType;
    private List<List<Thing>> allThinglist = new List<List<Thing>>();
    public List<Thing> ThingsToRegister = new List<Thing>();
    public List<Thing> ThingsToDeregister = new List<Thing>();

    public void Reset()
	{
		for (int i = 0; i < this.allThinglist.Count; i++)
		{
			this.allThinglist[i].Clear();
		}
		this.ThingsToRegister.Clear();
		this.ThingsToDeregister.Clear();
	}
    private List<Thing> BucketOf(Thing t)
	{
		int num = t.GetHashCode();
		if (num < 0)
		{
			num *= -1;
		}
		int index = num % this.TickInterval;
		return this.allThinglist[index];
	}
}
public enum TickType
{
    Never,
    Normal,
    Long,
    Rare
}
 
