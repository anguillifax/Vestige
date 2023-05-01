using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Angry : ActionNode
{
    protected override void OnStart()
    {
        if (blackboard.player == null)
        {
            context.agent.enabled = false;
            context.playAnimation("Angry", true);
        }
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {

        if (context.animator.GetBool("isInteracting") == true)
        {
            return State.Running;
        }
        return State.Success;
    }
}
