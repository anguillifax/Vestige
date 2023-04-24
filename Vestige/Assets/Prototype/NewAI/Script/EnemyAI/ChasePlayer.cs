using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ChasePlayer : ActionNode
{
    public float speed = 5;
    public float stoppingDistance = 1f;
    public float chaseDistance = 10f;
    public bool updateRotation = true;
    public float rotationSpeed = 10f;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;
    protected override void OnStart()
    {
        if (blackboard.player == null)
            return;

        context.agent.enabled = true;
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;

        context.agent.updateRotation = updateRotation;
        context.agent.acceleration = acceleration;
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

        if (blackboard.player == null)
            return State.Failure;
        context.agent.destination = blackboard.player.position;
        float distanceFromPlayer = Vector3.Distance(context.transform.position, blackboard.player.position);
        if (distanceFromPlayer >= chaseDistance)
        {
            HandleLostPlayer();
            return State.Failure;
        }

        else if (distanceFromPlayer <= tolerance)
        {
            return State.Success;
        }
        ChasingPlayer();
        return State.Running;
    }
    private void ChasingPlayer()
    {
        context.animator.SetFloat("Vertical", 1f);
        Vector3 direction = blackboard.player.position - context.transform.position;
        direction.y = 0;
        direction.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);

    }
    private void HandleLostPlayer()
    {
        blackboard.player = null;
    }

}
