using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FindPlayer : ActionNode
{

	public float detectionRadius = 20f;
	public float minDetectionAngle = -60f;
	public float maxDetectionAngle = 60f;
	public float stoppingDistance = 1.5f;
	public float RotationSpeed = 15f;
	public LayerMask playerMask;
	private bool isPlayerInSight;
	protected override void OnStart()
	{
		context.agent.enabled = false;
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

		isPlayerInSight = false;
		Collider[] colliders = Physics.OverlapSphere(context.transform.position, detectionRadius,playerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			Debug.Log(colliders);
			Transform player = colliders[i].transform;
			// DP_CharacterStats characterStats = colliders[i].transform.GetComponent<DP_CharacterStats>();
			if (player != null && player.tag == "Player")
			{
				Debug.Log("find player");
				Vector3 targetDirection = player.position - context.transform.position;
				float viewAbleAngle = Vector3.Angle(targetDirection, context.transform.forward);

				if (viewAbleAngle < maxDetectionAngle && viewAbleAngle > minDetectionAngle)
				{

					if (blackboard.player == null)
					{
						Activated();
						blackboard.player = player;
					}
					isPlayerInSight = true;

				}
			}
		}

		if (context.animator.GetBool("isInteracting") == true)
		{
			return State.Running;
		}
		return (isPlayerInSight) ? State.Success : State.Failure;
	}
	private void Activated()
	{
		context.playAnimation("noticedPlayer", true);
	}
}
