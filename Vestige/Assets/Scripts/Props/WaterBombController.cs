using FMODUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige
{
	public class WaterBombController : MonoBehaviour
	{
		// =========================================================
		// Types
		// =========================================================

		public enum State
		{
			Idle, Burning, Exploding, Consumed,
		}

		// =========================================================
		// Variables
		// =========================================================

		[Header("Common")]
		public ManualTimer countdown = new ManualTimer(3);
		public bool allowIgniteToCauseExplosion = true;

		[Header("Sound")]
		public StudioEventEmitter evPickup;
		public StudioEventEmitter evThrow;
		public StudioEventEmitter evPullPin;
		public StudioEventEmitter evExplode;

		private State state;
		private WaterBombSimpleAvatar avatar;
		private StandardRecipient effects;
		private StandardHoldable holdable;
		private StandardThrowable throwable;
		private RadialExplosion explosion;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			avatar = GetComponent<WaterBombSimpleAvatar>();
			effects = GetComponent<StandardRecipient>();
			holdable = GetComponent<StandardHoldable>();
			throwable = GetComponent<StandardThrowable>();
			explosion = GetComponent<RadialExplosion>();

			holdable.attached.AddListener(OnAttach);

			state = State.Idle;
		}

		// =========================================================
		// Dynamics
		// =========================================================

		private void OnAttach()
		{
			if (state == State.Burning)
			{
				holdable.InstructionOverlay.GetComponent<WaterBombOverlay>().SetHasActivated();
			}
			evPickup.Play();
		}

		private void Update()
		{
			switch (state)
			{
				case State.Idle: UpdateIdle(); break;
				case State.Burning: UpdateBurning(); break;
				case State.Exploding: UpdateExploding(); break;
				case State.Consumed: /* Ensure No Action */ break;
			}

		}

		private void UpdateIdle()
		{
			if (holdable.IsHeld && holdable.InputState.SecondaryDown)
			{
				throwable.ThrowObject();
				countdown.Start();

				avatar.Activate();
				evThrow.Play();
				evPullPin.Play();

				state = State.Burning;
				return;
			}

			bool activateCondition =
				(holdable.IsHeld && holdable.InputState.PrimaryDown) ||
				(allowIgniteToCauseExplosion && effects.effects.Any(x => x.ignite));
			if (activateCondition)
			{
				countdown.Start();

				evPullPin.Play();
				avatar.Activate();

				if (holdable.IsHeld)
				{
					holdable.InstructionOverlay.GetComponent<WaterBombOverlay>().SetHasActivated();
				}

				state = State.Burning;
			}
		}

		private void UpdateBurning()
		{
			countdown.Update(Time.deltaTime);

			if (holdable.IsHeld && holdable.InputState.SecondaryDown)
			{
				throwable.ThrowObject();
				evThrow.Play();
			}

			if (countdown.Done)
			{
				state = State.Exploding;
			}
		}

		private void UpdateExploding()
		{
			if (holdable.IsHeld)
			{
				holdable.Harness.Detach();
			}

			explosion.Explode();
			avatar.Detonate();

			evPullPin.Stop();
			evExplode.Play();

			state = State.Consumed;
		}

	}
}