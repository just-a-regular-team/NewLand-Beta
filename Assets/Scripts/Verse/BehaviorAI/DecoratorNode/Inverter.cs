using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree 
{
    public class Inverter : DecoratorNode {
        public override void Start() {
        }

        public override void Stop() {
        }

        public override State Update() {
            if (child == null) {
                return State.Failure;
            }

            switch (child.Update()) {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Success;
                case State.Success:
                    return State.Failure;
            }
            return State.Failure;
        }
    }
}