using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree
{
    public class SequencerNode : CompositeNode
    {
        int curr;
        public override void Start()
        {
            curr = 0;
        }
        public override void Stop()
        {

        }
        public override State Update()
        {
            var child = childs[curr];
            switch (child.StateUpdate())
            {
                case State.Running:
                {
                    return State.Running;
                }
                case State.Failure:
                {
                    return State.Failure;
                }
                case State.Success:
                {
                    curr++;
                    break;
                }
                default:
                    break;
            }
            
            return curr == childs.Count ? State.Success : State.Running;
        }
    }
}

