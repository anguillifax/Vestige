using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Locomotion : MonoBehaviour
{
	[Header("PathFinding settings")]
	public Transform player;
	public float FOV;
	public float distanceFromTarget;
	public float chaseDistance;
	AIDestinationSetter agent;
	void Start()
	{
		agent = GetComponent<AIDestinationSetter>();

	}

	// Update is called once per frame
	void Update()
	{
		distanceFromTarget = Vector3.Distance(transform.position, player.position);
		Vector3 targetDirection = player.position - transform.position;
		float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);

		if (viewAbleAngle < FOV && viewAbleAngle > -FOV && distanceFromTarget < chaseDistance)
		{
			agent.target = player;
		}

	}
}
