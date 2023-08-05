using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree 
{
    public class InterruptSelector : Selector {
        public override State Update() {
            int previous = current;
            base.Start();
            var status = base.Update();
            if (previous != current) {
                if (childs[previous].state == State.Running) {
                    //childs[previous].Abort();
                    Debug.LogError("haven't done yet");
                }
            }

            return status;
        }
    }
}