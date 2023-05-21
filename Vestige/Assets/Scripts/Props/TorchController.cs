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
		public ManualTimer throwBurningTimer = new ManualTimer(2);
		public SystemicEffectTemplate throwEffect;

		private State state;
		private StandardHoldable holdable;
		private StandardThrowable throwable;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			holdable = GetComponent<StandardHoldable>();
			throwable = GetComponent<StandardThrowable>();
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
			if (holdable.IsHeld) UpdateHoldable();

			cooldown.Update(Time.deltaTime);
			swipeDuration.Update(Time.deltaTime);
			throwBurningTimer.Update(Time.deltaTime);
		}

		private void UpdateState()
		{
			switch (state)
			{
				case State.Idle:
					if (holdable.IsHeld && holdable.InputState.PrimaryDown)
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
			if (holdable.InputState.SecondaryDown)
			{
				throwable.ThrowObject();
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