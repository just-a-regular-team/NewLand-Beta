using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree 
{
    public class RandomFailure : ActionNode 
    {

        public float chanceOfFailure = 0.5f;

        public override void Start() {
        }

        public override void Stop() {
        }

        public override State Update() {
            float value = Random.value;
            if (value > chanceOfFailure) {
                return State.Failure;
            }
            return State.Success;
        }
    }
}