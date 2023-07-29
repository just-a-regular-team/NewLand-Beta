using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThingWithComp : Thing
{
    #region Method of ThingComponent
    public List<ThingComponent> AllComps
	{
		get
		{
			return this.comps;
		}
	}
    public IEnumerable<T> GetComps<T>() where T : ThingComponent
    {
        if (this.comps != null)
        {
            int num;
            for (int i = 0; i < this.comps.Count; i = num + 1)
            {
                T t = this.comps[i] as T;
                if (t != null)
                {
                    yield return t;
                }
                num = i;
            }
        }
        yield break;
    }
    public void InitializeComps(Thing thing)
    {
        if (this.data.comps.Any<ThingComponent>())
        {
            this.comps = new List<ThingComponent>();
            for (int i = 0; i < this.data.comps.Count; i++)
            {
                ThingComponent thingComp = null;
                try
                {
                    thingComp = (ThingComponent)Activator.CreateInstance(this.data.comps[i].GetType());
                    thingComp.thing = this;
                    this.comps.Add(thingComp);
                    thingComp.Init(this);
                }
                catch (Exception arg)
                {
                    Debug.LogError("Could not instantiate or initialize a ThingComp: " + arg);
                    this.comps.Remove(thingComp);
                }
            }
        }
    }
    public override void SpawnSetup(Map map)
    {
        base.SpawnSetup(map);
        if (this.comps != null)
        {
            for (int i = 0; i < this.comps.Count; i++)
            {
                this.comps[i].Spawn();
            }
        }
    }
    public override void DeSpawn()
    {
        Map map = base.Map;
        base.DeSpawn();
        if (this.comps != null)
        {
            for (int i = 0; i < this.comps.Count; i++)
            {
                this.comps[i].DeSpawn(map);
            }
        }
    }
    public override void Destroy()
    {
         
        base.Destroy();
        if (this.comps != null)
        {
            for (int i = 0; i < this.comps.Count; i++)
            {
                this.comps[i].Destroy();
            }
        }
    }
    public override void Tick()
	{
		if (this.comps != null)
		{
			int i = 0;
			int count = this.comps.Count;
			while (i < count)
			{
				this.comps[i].TickNormal();
				i++;
			}
		}
	}

	public override void TickRare()
	{
		if (this.comps != null)
		{
			int i = 0;
			int count = this.comps.Count;
			while (i < count)
			{
				this.comps[i].TickRare();
				i++;
			}
		}
	}

	public override void TickLong()
	{
		if (this.comps != null)
		{
			int i = 0;
			int count = this.comps.Count;
			while (i < count)
			{
				this.comps[i].TickLong();
				i++;
			}
		}
	}
    #endregion
    public List<ThingComponent> comps;
}
