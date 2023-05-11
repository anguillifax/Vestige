using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class StandardBurnable : MonoBehaviour
	{
		// =========================================================
		// Types
		// =========================================================

		public enum State
		{
			Waiting, Burning, Destroyed
		}

		// =========================================================
		// Variables
		// =========================================================

		private static GameObject prefabFlames;

		[Header("Main")]
		public bool canBurn = true;
		public ManualTimer burnDuration = new ManualTimer(3);

		[Header("Burn")]
		public UnityEvent ignited;
		public bool useDefaultFlameEffect = true;
		public bool spawnFlameAsChild = false;

		[Header("Propagation")]
		public bool propagateIgnite = true;
		public float propagateIgniteRadius = 3;

		[Header("Destruction")]
		public UnityEvent burned;
		public bool destroyOnBurned = true;

		private State state;
		private StandardRecipient receiver;
		private GameObject flameEffect;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			if (prefabFlames == null)
			{
				prefabFlames = Resources.Load<GameObject>("StandardBurnable Flames");
			}

			receiver = GetComponent<StandardRecipient>();

			state = canBurn ? State.Waiting : State.Destroyed;
		}

		// =========================================================
		// Update
		// =========================================================

		private void Update()
		{
			switch (state)
			{
				case State.Waiting:
					if (receiver.effects.Any(x => x.ignite))
					{
						ignited.Invoke();
						if (useDefaultFlameEffect)
						{
							flameEffect = Instantiate(prefabFlames, transform.position, prefabFlames.transform.rotation);
							if (spawnFlameAsChild)
							{
								flameEffect.transform.parent = transform;
							}
						}
						burnDuration.Start();

						state = State.Burning;
					}
					break;

				case State.Burning:
					if (burnDuration.Done)
					{
						burned.Invoke();
						if (propagateIgnite)
						{
							FindAndSendIgnite();
						}
						if (flameEffect)
						{
							flameEffect.GetComponent<ParticleSystem>().Stop();
						}
						if (destroyOnBurned)
						{
							Destroy(gameObject);
						}

						state = State.Destroyed;
					}
					break;

				case State.Destroyed:
					break;
			}

			burnDuration.Update(Time.deltaTime);
		}

		public void Revive()
		{
			state = State.Waiting;
		}

		private void FindAndSendIgnite()
		{
			Collider[] targets = Physics.OverlapSphere(transform.position, propagateIgniteRadius, int.MaxValue, QueryTriggerInteraction.Collide);
			foreach (Collider c in targets)
			{
				var recipient = c.GetComponent<IRecipient>();
				if (recipient != null)
				{
					Effect effect = new Effect(gameObject)
					{
						ignite = true,
					};
					recipient.RecieveEffect(effect);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (propagateIgnite)
			{
				Color BrightOrange = new Color(240 / 255f, 134 / 255f, 22 / 255f);
				Gizmos.color = BrightOrange;
				Gizmos.DrawWireSphere(transform.position, propagateIgniteRadius);
			}
		}
	}
}