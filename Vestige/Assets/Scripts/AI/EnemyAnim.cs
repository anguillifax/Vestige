using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class EnemyAnim : MonoBehaviour
	{
		public Animator anim;
		public EnemyManager manager;
		public EnemyLocomotion locomotion;

		private void Awake()
		{
			anim = GetComponent<Animator>();
			manager = GetComponentInParent<EnemyManager>();
			locomotion = GetComponentInParent<EnemyLocomotion>();

		}

		public void ApplyTargetAnimation(string animationName, bool isInteracting)
		{
			// anim.applyRootMotion = isInteracting;
			anim.SetBool("isInteracting", isInteracting);
			anim.CrossFade(animationName, 0.2f);
		}

		public void AttackPlayerEvent()
		{
			float damage = manager.Damage;
			if (locomotion.CanDealDamage())
			{
				Debug.LogWarning("TODO Add Damage Attack Here", this);
			}
		}

		public void EnemyDieAnimation()
		{

		}
	}
}
