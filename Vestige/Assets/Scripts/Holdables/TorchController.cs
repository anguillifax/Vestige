using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class TorchController : MonoBehaviour, IHoldable
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

		void IHoldable.ReceiveInput(HoldableInputState input)
		{
			if (input.PrimaryDown)
			{
				Debug.Log("Primary action on torch");
			}

			if (input.SecondaryDown)
			{
				harness.Detach();
			}
		}

		void IHoldable.BindInstructionOverlay(GameObject spawnedObject)
		{
		}
	}
}