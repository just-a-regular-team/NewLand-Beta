using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ID
{
    public string GlobalID();
    public int UniqueID{get;protected set;}
}
