using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Vestige
{
	public class EnemyManager : MonoBehaviour
	{
		EnemyAnim anim;
		EnemyLocomotion locomotion;
		public PlayerController player;
		[Header("GamePlay")]
		[SerializeField] Transform walkStraightDestination;
		[SerializeField] Transform waterDestination;
		[SerializeField] float randomFleeRadius = 100.0f;
		[SerializeField] float timeBetweenMoves = 1.0f;
		[SerializeField] float timer;

		[Header("AI Setting")]
		public float detectionRadius = 20f;
		public float FOV = 60f;
		public NavMeshAgent agent;
		public float rotationSpeed = 15f;
		public float chaseSpeed = 5f;
		public float stopDistance = 1.2f;
		public float chaseDistance = 20f;
		public Transform currentTarget;
		public float distanceFromPlayer;

		[Header("Animation Control")]
		public float WalkBlend;
		public float RunBlend;
		public float FleeBlend;
		public bool isInteracting;
		[Header("Status")]
		public bool isOnFire;
		public bool isInCombat;
		public bool isDead;
		[Header("Combat Setting")]
		public float currentRecoveryTime = 0;
		public float Damage = 10f;
		[Header("State Manager")]
		public StateMachine currentState;
		public StateMachine InitialState;

		private int offset;

		private void Awake()
		{
			agent = GetComponent<NavMeshAgent>();
			anim = GetComponentInChildren<EnemyAnim>();
			locomotion = GetComponent<EnemyLocomotion>();
			player = FindObjectOfType<PlayerController>();
			offset = Random.Range(0, 10);
			timer = timeBetweenMoves;
		}
		
		void Update()
		{
			// HARDCODED
			// offset = (offset + 1) % 10;
			// if (offset == 0 && Vector3.Distance(player.transform.position, transform.position) < 10)
			// {
			// 	currentTarget = player.transform;
			// }

			isInteracting = anim.anim.GetBool("isInteracting");
			if (isDead)
			{
				agent.enabled = false;
				//play death animation
				return;
			}
			else if (!isOnFire && walkStraightDestination == null)
			{
				locomotion.ChasePlayer();
				locomotion.FacePlayer();
				if (locomotion.CanDealDamage())
				{
					anim.ApplyTargetAnimation("attack_02", true);
				}
			}
			else if (isOnFire && walkStraightDestination == null)
			{
				//walkstraight will not effect by anything
				OnFire(waterDestination);

			}
			else if (walkStraightDestination)
			{

				WalkStraight(walkStraightDestination);
			}

		}
		void WalkStraight(Transform destination)
		{
			if (Vector3.Distance(destination.position, transform.position) < 3f)
			{
				anim.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
			}
			else
			{
				anim.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
			}
			agent.enabled = true;
			agent.SetDestination(destination.position);
		}
		void OnFire(Transform waterPosition)
		{
			if (waterPosition == null)
			{
				agent.enabled = false;
				if (!isInteracting)
				{
					anim.ApplyTargetAnimation("catchFire", true);
				}

			}
			else
			{
				agent.enabled = true;
				anim.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
				agent.SetDestination(waterPosition.position);
			}

		}
		// private void WalkRandomly()
		// {
		// 	timer += Time.deltaTime;
		// 	if (timer >= timeBetweenMoves)
		// 	{
		// 		Vector3 newPosition = RandomNavSphere(transform.position, randomFleeRadius, -1);

		// 		agent.SetDestination(newPosition);
		// 		timer = 0;
		// 	}
		// }

		// public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
		// {
		// 	Vector3 randomDirection = Random.insideUnitSphere * distance;
		// 	randomDirection += origin;
		// 	NavMeshHit navHit;
		// 	NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
		// 	return navHit.position;
		// }
	}
}
