using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		void Start()
		{

		}

		// void Update()
		// {
		// 	// Same as above, but much cleaner
		// 	bool hasIgnite = systemic.effects
		// 		.Where(x => x.source != systemic.gameObject)
		// 		.Any(x => x.ignite);
		// }

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
