using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Flee : ActionNode
{
    public float fleeSpeed = 5f;
    private bool isFiredPlayed = false;
    private bool isReachedDestination = false;
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (!context.manager.isFired)
        {
            return State.Failure;
        }
        context.SetOnFire();
        if (context.animator.GetBool("isInteracting") == true)
        {
            return State.Running;
        }
        Fleeing();
        float distanceFromWater = Vector3.Distance(context.manager.waterSource.position, context.transform.position);
        if (distanceFromWater < 0.2f)
        {
            FireExtinguished();
        }

        return (isReachedDestination) ? State.Success : State.Running;
    }

    private void Fleeing()
    {
        context.agent.enabled = true;
        context.agent.speed = fleeSpeed;
        context.agent.SetDestination(context.manager.waterSource.position);
        context.animator.SetFloat("Vertical", 1.5f);
    }
    private void FireExtinguished()
    {
        context.playAnimation("shakeOffFire", true);
        context.manager.TurnOffFire();
        context.manager.isFired = false;
        isReachedDestination = true;
        context.manager.isFiredPlayed = false;
    }
}
