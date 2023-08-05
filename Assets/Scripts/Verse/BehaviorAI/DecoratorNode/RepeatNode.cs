using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree
{
    public class RepeatNode : DecoratorNode
    {
        public override void Start(){}

        public override void Stop(){}

        public override State Update()
        {
            child.StateUpdate();
            return State.Running;
        }
    }
}

