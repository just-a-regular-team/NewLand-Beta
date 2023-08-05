using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class WaitNode : ActionNode
    {
        public float duration;
        float startTime;

        public override void Start()
        {
            startTime = Time.time;
        }

        public override State Update()
        {
            if(Time.time - startTime > duration)
            {
                return State.Success;
            }
            return State.Running;
        }
    }
}
