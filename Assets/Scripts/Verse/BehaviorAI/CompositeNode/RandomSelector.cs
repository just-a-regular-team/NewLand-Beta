using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BehaviorTree 
{
    public class RandomSelector : CompositeNode {
        protected int current;

        public override void Start() {
            current = Random.Range(0, childs.Count);
        }

        public override void Stop() {
        }

        public override State Update() {
            var child = childs[current];
            return child.Update();
        }
    }
}