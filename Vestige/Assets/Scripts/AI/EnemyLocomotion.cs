using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class EnemyLocomotion : MonoBehaviour
	{
		private EnemyAnim anim;
		private EnemyManager manager;
		private Rigidbody enemyRigidbody;

		private void Awake()
		{
			enemyRigidbody = GetComponent<Rigidbody>();
			manager = GetComponent<EnemyManager>();
			anim = GetComponentInChildren<EnemyAnim>();
		}

		public bool CanChase()
		{
			if (manager.currentTarget == null)
			{
				return false;
			}
			manager.distanceFromPlayer = Vector3.Distance(manager.currentTarget.position, transform.position);

			return manager.distanceFromPlayer <= manager.chaseDistance
					&& manager.distanceFromPlayer >= manager.stopDistance
					&& !manager.isInteracting;
		}

		public bool CanDealDamage()
		{
			if (manager.currentTarget == null)
			{
				return false;
			}
			manager.distanceFromPlayer = Vector3.Distance(manager.currentTarget.position, transform.position);
			return (manager.distanceFromPlayer <= manager.stopDistance
					&& !manager.isInteracting);
		}

		public void ChasePlayer()
		{
			if (CanChase())
			{
				manager.agent.enabled = true;
				manager.agent.SetDestination(manager.currentTarget.position);
				manager.agent.speed = manager.chaseSpeed;
				anim.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
				enemyRigidbody.velocity = manager.agent.velocity;
			}
			else
			{
				manager.agent.enabled = false;
				anim.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
			}
		}

		public void FacePlayer()
		{
			if (manager.currentTarget == null)
			{
				return;
			}

			Vector3 direction = manager.currentTarget.position - transform.position;
			direction.y = 0;
			direction.Normalize();

			if (direction == Vector3.zero)
			{
				direction = transform.forward;
			}
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, manager.rotationSpeed / Time.deltaTime);
		}
	}
}
