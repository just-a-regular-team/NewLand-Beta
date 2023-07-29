using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ThingComponent
{
    public Thing thing;

    public virtual void Init(Thing thing){}
    public virtual void Spawn(){}
    public virtual void DeSpawn(Map map){}
    public virtual void Destroy(){}

    public virtual void TickNormal(){}
    public virtual void TickRare(){}
    public virtual void TickLong(){}

    public virtual void Draw(){}
     
}

