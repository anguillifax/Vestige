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

		public GameObject instructionOverlay;
		public Collider solidCollider;

		private HoldableHarness harness;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
		}

		// =========================================================
		// IHoldable Implementation
		// =========================================================

		public GameObject Object => gameObject;

		public void OnPickup(HoldableHarness harness)
		{
			transform.rotation = harness.InitialRotation;
			GetComponent<Rigidbody>().isKinematic = true;
			solidCollider.enabled = false;
			this.harness = harness;
		}

		public void OnDrop()
		{
			GetComponent<Rigidbody>().isKinematic = false;
			solidCollider.enabled = true;
			harness = null;
		}

		public void ActivatePrimary(IHoldable.InputPhase phase)
		{
		}

		public void ActivateSecondary(IHoldable.InputPhase phase)
		{
			harness.Detach();
		}

		public GameObject GetInstructionOverlay() => instructionOverlay;

		public void BindInstructionOverlay(GameObject spawnedObject)
		{
		}
	}
}