using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class CompositeNode : Node
    {
        public List<Node> childs = new();
    }    
}

