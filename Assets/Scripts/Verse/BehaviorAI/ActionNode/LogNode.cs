using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class LogNode : ActionNode
    {   
        public static string prefixFmt = "<color={0}>{1}</color>";
        public string[] colors = new string[]
	    {
		"yellow",
        "green",
		"red"
	    };
        public int lvl = 0;

        public string messager;
        string result()
        {
            return string.Format(prefixFmt, colors[(int)lvl], messager.ToString());
        }
        string result(int lvl)
        {
            return string.Format(prefixFmt, colors[(int)lvl], messager.ToString());
        }


        public override State Update()
        {
            Debug.Log(result());
            return State.Success;
        }
    }
}
