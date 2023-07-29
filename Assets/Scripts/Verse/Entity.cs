using System;

public abstract class Entity
{
    public abstract string Name{get;set;}

    public abstract void SpawnSetup(Map map);
    public abstract void DeSpawn();

    public virtual void Tick()
	{
		throw new NotImplementedException();
	}
    public virtual void TickRare()
	{
		throw new NotImplementedException();
	}
	public virtual void TickLong()
	{
		throw new NotImplementedException();
	}
}
