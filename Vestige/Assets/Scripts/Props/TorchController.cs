using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class TorchController : MonoBehaviour
	{
		// =========================================================
		// Types
		// =========================================================

		public enum State
		{
			Idle, Swipe, Cooldown,
		}

		// =========================================================
		// Data
		// =========================================================

		[Header("Common")]
		public ManualTimer cooldown = new ManualTimer(0.5f);

		[Header("Swipe")]
		public ManualTimer swipeDuration = new ManualTimer(0.2f);
		public BoxCollider swipeArea;
		public SystemicEffectTemplate swipeEffect;
		public UnityEvent swipeStarted;

		[Header("Throw")]
		public float throwAngle = 30f;
		public float throwVelocity = 6f;
		public float inheritFactor = 1;

		private State state;
		private StandardHoldable holdable;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			holdable = GetComponent<StandardHoldable>();
			holdable.attached.AddListener(PickupReset);
			PickupReset();
		}

		private void PickupReset()
		{
			state = State.Idle;
		}

		// =========================================================
		// Logic Update
		// =========================================================

		private void Update()
		{
			if (holdable.Active) UpdateHoldable();

			cooldown.Update(Time.deltaTime);
			swipeDuration.Update(Time.deltaTime);
		}

		private void UpdateHoldable()
		{
			switch (state)
			{
				case State.Idle:
					if (holdable.input.PrimaryDown)
					{
						swipeDuration.Start();
						swipeStarted.Invoke();
						state = State.Swipe;
					}
					break;

				case State.Swipe:
					if (swipeDuration.Done)
					{
						cooldown.Start();
						state = State.Cooldown;
					}
					break;

				case State.Cooldown:
					if (cooldown.Done)
					{
						state = State.Idle;
					}
					break;
			}

			if (holdable.input.SecondaryDown)
			{
				Vector3 localDir = Quaternion.Euler(-throwAngle, 0, 0) * Vector3.forward;
				Vector3 outDir = holdable.transform.TransformDirection(localDir);

				Vector3 vel = outDir * throwVelocity;

				Rigidbody ownerBody = holdable.harness.Owner.GetComponent<Rigidbody>();
				if (ownerBody != null)
				{
					vel += ownerBody.velocity * inheritFactor;
				}

				holdable.harness.Detach();
				state = State.Cooldown;

				GetComponent<Rigidbody>().velocity = vel;
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (state == State.Swipe && other.attachedRigidbody != null)
			{
				IRecipient systemic = other.attachedRigidbody.GetComponent<IRecipient>();
				if (systemic != null)
				{
					Effect e = swipeEffect.AsEffect(gameObject);
					systemic.RecieveEffect(e);
				}
			}
		}
	}
}