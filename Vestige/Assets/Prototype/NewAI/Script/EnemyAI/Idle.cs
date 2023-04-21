using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Idle : ActionNode
{
    [Header("Locomotion")]
    public float walkSpeed = 5f;
    public float toleranceFromInitalPoint = 0.5f;

    [Header("Rotation settings")]
    public float RotationSpeed = 6f;
    public bool isRotating;


    protected override void OnStart()
    {
        blackboard.player = null;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        #region HandleSpecialEffects
        context.CheckIsFired();
        if (context.manager.isFired
        || context.PlayerInSight())
        {
            Debug.Log("find player while idle" + context.PlayerInSight());
            return State.Failure;
        }

        #endregion


        float distanceFromInitial = Vector3.Distance(context.transform.position, context.manager.restSpot);
        if (distanceFromInitial < toleranceFromInitalPoint)
        {
            Rotating();
            if (isRotating)
            {
                return State.Running;
            }
            Resting();
            return State.Success;
        }

        else
        {
            WalkBack();
        }
        return State.Running;
    }
    private void WalkBack()
    {
        context.agent.enabled = true;
        context.agent.speed = 5;
        context.agent.SetDestination(context.manager.restSpot);

        context.animator.SetFloat("Vertical", 0.5f);
    }
    private void Resting()
    {
        context.agent.enabled = false;
        context.playAnimation("Happy", false);
        context.animator.SetFloat("Vertical", 0f);
    }
    private void Rotating()
    {
        context.agent.enabled = false;

        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, context.manager.initialRotation, RotationSpeed / Time.deltaTime);

        isRotating = (Quaternion.Angle(context.transform.rotation, context.manager.initialRotation) > 2);

    }
}
