using System;
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

		public GameObject Object => gameObject;
		public HoldableConfig Config => config;

		public void OnPickup(HoldableHarness harness)
		{
			physicsHelper.Attach(harness.Socket);
			this.harness = harness;
		}

		public void OnDrop()
		{
			physicsHelper.Detach(harness.DropPoint);
			harness = null;
		}

		public void ActivatePrimary(IHoldable.InputPhase phase)
		{
			switch (phase)
			{
				case IHoldable.InputPhase.Start:
					Debug.Log("Throw torch");
					break;

				case IHoldable.InputPhase.Hold:
					break;

				case IHoldable.InputPhase.Stop:
					break;
			}
		}

		public void ActivateSecondary(IHoldable.InputPhase phase)
		{
			switch (phase)
			{
				case IHoldable.InputPhase.Start:
					harness.Detach();
					break;

				case IHoldable.InputPhase.Hold:
					break;

				case IHoldable.InputPhase.Stop:
					break;
			}
		}

		public void BindInstructionOverlay(GameObject spawnedObject)
		{
		}
	}
}