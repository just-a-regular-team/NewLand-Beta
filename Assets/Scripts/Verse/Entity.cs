using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public abstract string Name{get;set;}
    public abstract string Label{get;set;}

    

    public abstract void SpawmSetup();
    public abstract void Despawm();
}
