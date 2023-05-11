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

		private void Awake()
		{
			agent = GetComponent<NavMeshAgent>();
			anim = GetComponentInChildren<EnemyAnim>();
			locomotion = GetComponent<EnemyLocomotion>();
			player = FindObjectOfType<PlayerController>();
		}
		void Start()
		{
			if (player != null && currentTarget != null)
			{
				currentTarget = player.transform;
			}

		}

		// Update is called once per frame
		void Update()
		{
			isInteracting = anim.anim.GetBool("isInteracting");
			locomotion.ChasePlayer();
			locomotion.facePlayer();
			if (locomotion.CanDealDamage())
			{
				anim.ApplyTargetAnimation("attack_02", true);
			}
		}
	}
}
