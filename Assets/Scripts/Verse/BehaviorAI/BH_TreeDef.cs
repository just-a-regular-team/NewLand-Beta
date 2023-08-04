using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BH_TreeDef
    {
        public Node rootNode;
        public Node.State treeState;
        public Node.State UpdateBHTree()
        {
            if(rootNode.state == Node.State.Running)
            {
                treeState = rootNode.StateUpdate();
            }
            return treeState;
        }
    }
}
