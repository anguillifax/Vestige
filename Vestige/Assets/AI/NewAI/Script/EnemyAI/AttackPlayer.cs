using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class AttackPlayer : ActionNode
{
    protected override void OnStart()
    {
        context.agent.enabled = false;
        context.animator.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
        context.playAnimation("attack_01", true);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        #region HandleSpecialEffects
        context.CheckIsFired();
        if (context.manager.isFired)
            return State.Failure;
        #endregion

        if (context.animator.GetBool("isInteracting") == true)
        {
            return State.Running;
        }

        return State.Success;
    }
}
