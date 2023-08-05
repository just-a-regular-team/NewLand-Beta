using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class Behavior : MonoBehaviour
{
    BH_TreeDef treeDef;
    void Start()
    {
        treeDef = new BH_TreeDef();
        var log = new LogNode();
        log.messager = "Hello world";
        var log1 = new LogNode();
        log1.messager = "Hello world 1";
        var log2 = new LogNode();
        log2.messager = "Hello world 2";

        var wait = new WaitNode();
        wait.duration = 2;

        var SequencerNode = new SequencerNode();

        var repeat = new RepeatNode();
        repeat.child = SequencerNode;

        SequencerNode.childs = new List<Node>()
        {
            log,wait,log1,wait,log2,wait
        };

        treeDef.rootNode = repeat;
    }
    void Update()
    {
        treeDef.UpdateBHTree();
    }
}
