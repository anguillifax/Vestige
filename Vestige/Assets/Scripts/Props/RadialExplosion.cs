using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class RadialExplosion : MonoBehaviour
	{
		// =========================================================
		// Variables
		// =========================================================

		private readonly Color DrawColor = new Color(255 / 255f, 111 / 255f, 59 / 255f);

		[Header("Common")]
		public float radius = 10;
		public SystemicEffectTemplate template;
		public LayerMask mask;
		public float explosionForce = 0.5f;
		public float upwardsModifier = 1.2f;

		[Header("Test")]
		public InspectorCallbackButton testExplode = new InspectorCallbackButton("Trigger Explosion");

		// =========================================================
		// Initialize
		// =========================================================

		private void Awake()
		{
			testExplode.callback = Explode;
		}

		// =========================================================
		// Public Interface
		// =========================================================

		public void Explode()
		{
			var hits = Physics.OverlapSphere(transform.position, radius, mask, QueryTriggerInteraction.Collide);
			SystemicUtil.BroadcastToRigidbody(template, gameObject, hits);

			foreach (Collider col in hits)
			{
				if (col.attachedRigidbody != null)
				{
					col.attachedRigidbody.AddExplosionForce(
						explosionForce, transform.position, radius, upwardsModifier, ForceMode.VelocityChange);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = DrawColor;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
	}
}