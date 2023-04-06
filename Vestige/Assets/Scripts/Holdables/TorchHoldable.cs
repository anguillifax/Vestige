﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class TorchHoldable : MonoBehaviour, IHoldable
	{
		// =========================================================
		// Data
		// =========================================================

		public HoldableConfig config;

		private HoldableHarness harness;
		private HoldablePhysicsHelper physicsHelper;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			physicsHelper = GetComponent<HoldablePhysicsHelper>();
		}

		// =========================================================
		// IHoldable Implementation
		// =========================================================

		GameObject IHoldable.Object => gameObject;
		HoldableConfig IHoldable.Config => config;

		void IHoldable.OnPickup(HoldableHarness harness)
		{
			physicsHelper.Attach(harness.Socket);
			this.harness = harness;
		}

		void IHoldable.OnDrop()
		{
			physicsHelper.Detach(harness.DropPoint);
			harness = null;
		}

		void IHoldable.ActivatePrimary(HoldableInputPhase phase)
		{
			switch (phase)
			{
				case HoldableInputPhase.Start:
					Debug.Log("Throw torch");
					break;

				case HoldableInputPhase.Hold:
					break;

				case HoldableInputPhase.Stop:
					break;
			}
		}

		void IHoldable.ActivateSecondary(HoldableInputPhase phase)
		{
			switch (phase)
			{
				case HoldableInputPhase.Start:
					harness.Detach();
					break;

				case HoldableInputPhase.Hold:
					break;

				case HoldableInputPhase.Stop:
					break;
			}
		}

		void IHoldable.BindInstructionOverlay(GameObject spawnedObject)
		{
		}
	}
}