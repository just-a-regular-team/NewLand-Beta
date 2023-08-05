using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree 
{
    public class Selector : CompositeNode {
        protected int current;

        public override void Start() {
            current = 0;
        }

        public override void Stop() {
        }

        public override State Update() {
            for (int i = current; i < childs.Count; ++i) {
                current = i;
                var child = childs[current];

                switch (child.Update()) {
                    case State.Running:
                        return State.Running;
                    case State.Success:
                        return State.Success;
                    case State.Failure:
                        continue;
                }
            }

            return State.Failure;
        }
    }
}