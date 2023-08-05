using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Timeout : DecoratorNode 
    {
        public float duration = 1.0f;
        float startTime;

        public override void Start() {
            startTime = Time.time;
        }

        public override void Stop() {
        }

        public override State Update() {
            if (child == null) {
                return State.Failure;
            }

            if (Time.time - startTime > duration) {
                return State.Failure;
            }

            return child.Update();
        }
    }
}