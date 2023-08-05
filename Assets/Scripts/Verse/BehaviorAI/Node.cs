using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Node
    {
        public enum State 
        {
            Running,
            Failure,
            Success
        }

        public State state = State.Running;
        public bool isStarted = false;

        public State StateUpdate()
        {
            if(!isStarted)
            {
                isStarted = true;
                Start();
            }
            state = Update();
            if(state == State.Success ||state == State.Failure)
            {
                Stop();
                isStarted = false;
            }
            
            return state;
        }
        

        public virtual void Start(){}
        public virtual void Stop(){}
        public abstract State Update();
    }
}
