using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class EnemyAttackWrapper : MonoBehaviour
	{
		[Header("Position")]
		public Transform hand;
		public float radius = 2;

		[Header("Damage")]
		public float delay = 1;
		public SystemicEffectTemplate template;
		public LayerMask mask = 1;

		public void Attack()
		{
			Invoke(nameof(AttackInner), delay);
		}

		private void AttackInner()
		{
			var hits = Physics.OverlapSphere(hand.position, radius, mask);
			SystemicUtil.BroadcastToRigidbody(template, gameObject, hits);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(hand.position, radius);
		}
	}
}