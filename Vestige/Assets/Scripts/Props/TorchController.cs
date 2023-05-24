﻿using System;
using System.Collections.Generic;
using System.Linq;
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
			Idle, Swipe, Cooldown, Thrown, Extinguished,
		}

		// =========================================================
		// Data
		// =========================================================

		[Header("Common")]
		public bool startLit = true;
		public ManualTimer cooldown = new ManualTimer(0.5f);

		[Header("Swipe")]
		public ManualTimer swipeDuration = new ManualTimer(0.2f);
		public BoxCollider swipeArea;
		public SystemicEffectTemplate swipeEffect;
		public UnityEvent swipeStarted;
		public UnityEvent swipeEnded;

		[Header("Throw")]
		public ManualTimer throwBurningTimer = new ManualTimer(2);
		public SystemicEffectTemplate throwEffect;

		private State state;
		private TorchAvatar avatar;
		private StandardHoldable holdable;
		private StandardThrowable throwable;
		private StandardRecipient systemic;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			avatar = GetComponent<TorchAvatar>();
			holdable = GetComponent<StandardHoldable>();
			throwable = GetComponent<StandardThrowable>();
			systemic = GetComponent<StandardRecipient>();

			holdable.attached.AddListener(PickupReset);
			PickupReset();
		}

		private void PickupReset()
		{
			if (!(state == State.Idle || state == State.Extinguished))
			{
				state = State.Idle;
			}
		}

		// =========================================================
		// Logic Update
		// =========================================================

		private void Update()
		{
			UpdateState();

			cooldown.Update(Time.deltaTime);
			swipeDuration.Update(Time.deltaTime);
			throwBurningTimer.Update(Time.deltaTime);
		}

		private void UpdateState()
		{
			switch (state)
			{
				case State.Idle:
					if (systemic.effects.Any(x => x.douse))
					{
						GotoExtinguished();
						break;
					}

					if (holdable.IsHeld && holdable.InputState.PrimaryDown)
					{
						swipeDuration.Start();
						swipeStarted.Invoke();
						throwBurningTimer.Stop();
						state = State.Swipe;
						break;
					}

					if (holdable.IsHeld && holdable.InputState.SecondaryDown)
					{
						throwable.ThrowObject();
						state = State.Thrown;
					}
					break;

				case State.Swipe:
					if (swipeDuration.Done)
					{
						cooldown.Start();
						swipeEnded.Invoke();
						state = State.Cooldown;
					}
					break;

				case State.Cooldown:
					if (systemic.effects.Any(x => x.douse))
					{
						cooldown.Stop();
						GotoExtinguished();
						break;
					}

					if (cooldown.Done)
					{
						state = State.Idle;
					}
					break;

				case State.Thrown:
					if (systemic.effects.Any(x => x.douse))
					{
						GotoExtinguished();
						break;
					}

					if (throwBurningTimer.Done)
					{
						state = State.Idle;
					}
					break;

				case State.Extinguished:
					if (systemic.effects.Any(x => x.ignite && x.source != gameObject))
					{
						avatar.Ignite();
						state = State.Idle;
					}

					if (holdable.IsHeld && holdable.InputState.SecondaryDown)
					{
						throwable.ThrowObject();
					}

					break;
			}
		}

		private void GotoExtinguished()
		{
			avatar.Extinguish();
			state = State.Extinguished;
		}

		private void OnTriggerStay(Collider other)
		{
			switch (state)
			{
				case State.Swipe:
					SystemicUtil.BroadcastToRigidbody(swipeEffect, gameObject, other);
					break;

				case State.Thrown:
					SystemicUtil.BroadcastToRigidbody(throwEffect, gameObject, other);
					break;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (state == State.Thrown)
			{
				throwBurningTimer.Start();
			}
		}
	}
}