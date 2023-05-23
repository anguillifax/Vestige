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

		[Header("Doused")]
		public UnityEvent doused;
		public bool canDouse = true;

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
					if (receiver.effects.Any(x => x.ignite) && !receiver.effects.Any(x => x.soak || x.douse))
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
					if (canDouse && receiver.effects.Any(x => x.douse))
					{
						doused.Invoke();
						if (flameEffect)
						{
							flameEffect.GetComponent<ParticleSystem>().Stop();
						}
						state = State.Waiting;
						break;
					}

					if (burnDuration.Done)
					{
						burned.Invoke();
						if (propagateIgnite)
						{
							FindAndSendEffect(CreateIgnite);
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

		private void FixedUpdate()
		{
			if (state == State.Burning)
			{
				FindAndSendEffect(CreateBurn);
			}
		}

		private Effect CreateIgnite()
		{
			return new Effect(gameObject) { burn = true, ignite = true };
		}

		private Effect CreateBurn()
		{
			return new Effect(gameObject) { burn = true };
		}

		private void FindAndSendEffect(Func<Effect> generator)
		{
			Collider[] targets = Physics.OverlapSphere(
				transform.position, propagateIgniteRadius, int.MaxValue, QueryTriggerInteraction.Collide);

			foreach (IRecipient r in SystemicUtil.GetRecipients(targets))
			{
				r.RecieveEffect(generator());
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