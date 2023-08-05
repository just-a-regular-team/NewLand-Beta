using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Succeed : DecoratorNode {
        public override void Start() {
        }

        public override void Stop() {
        }

        public override State Update() {
            if (child == null) {
                return State.Failure;
            }

            var state = child.Update();
            if (state == State.Failure) {
                return State.Success;
            }
            return state;
        }
    }
}