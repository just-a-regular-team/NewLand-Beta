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
			switch (this.tickType)
			{
			case TickType.Normal:
				return 1;
			case TickType.Rare:
				return 250;
			case TickType.Long:
					return 2000;
			default:
				return -1;
			}
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
 
