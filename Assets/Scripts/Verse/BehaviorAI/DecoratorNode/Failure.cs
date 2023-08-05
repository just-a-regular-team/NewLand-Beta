using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Failure : DecoratorNode {
        public override void Start() {
        }

        public override void Stop() {
        }

        public override State Update() {
            if (child == null) {
                return State.Failure;
            }

            var state = child.Update();
            if (state == State.Success) {
                return State.Failure;
            }
            return state;
        }
    }
}