using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class DecoratorNode : Node
    {
        public Node child;
    }
}

