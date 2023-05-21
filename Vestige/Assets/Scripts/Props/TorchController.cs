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
			Idle, Swipe, Cooldown, Thrown,
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
		public ManualTimer throwBurningTimer = new ManualTimer(2);
		public SystemicEffectTemplate throwEffect;

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
			UpdateState();
			if (holdable.Active) UpdateHoldable();

			cooldown.Update(Time.deltaTime);
			swipeDuration.Update(Time.deltaTime);
			throwBurningTimer.Update(Time.deltaTime);
		}

		private void UpdateState()
		{
			switch (state)
			{
				case State.Idle:
					if (holdable.Active && holdable.input.PrimaryDown)
					{
						swipeDuration.Start();
						swipeStarted.Invoke();
						throwBurningTimer.Stop();
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

				case State.Thrown:
					if (throwBurningTimer.Done)
					{
						state = State.Idle;
					}
					break;
			}
		}

		private void UpdateHoldable()
		{
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

				GetComponent<Rigidbody>().velocity = vel;

				throwBurningTimer.Start();
				state = State.Thrown;
			}
		}

		private void OnTriggerStay(Collider other)
		{
			switch (state)
			{
				case State.Swipe:
					SystemicUtil.BroadcastIfExists(swipeEffect, gameObject, other);
					break;

				case State.Thrown:
					SystemicUtil.BroadcastIfExists(throwEffect, gameObject, other);
					break;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (throwBurningTimer.Running)
			{
				throwBurningTimer.Start();
			}
		}
	}
}