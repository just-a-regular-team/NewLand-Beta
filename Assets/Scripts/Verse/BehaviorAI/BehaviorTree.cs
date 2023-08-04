using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class Behavior
{
    BH_TreeDef treeDef;
    void Start()
    {
        treeDef = new BH_TreeDef();
    }
    void Update()
    {
        treeDef.UpdateBHTree();
    }
}
