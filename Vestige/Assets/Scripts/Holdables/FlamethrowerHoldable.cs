using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class FlamethrowerHoldable : MonoBehaviour, IHoldable
	{
		// =========================================================
		// Data
		// =========================================================

		[Header("Common")]
		public FlamethrowerAvatar avatar;
		public HoldableConfig config;
		public float maxAmmo = 20;
		public float refillRate = 6;
		public float consumeRate = 3;

		[Header("Runtime")]
		public float currentAmmo;

		private HoldablePhysicsHelper physicsHelper;
		private HoldableHarness harness;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			physicsHelper = GetComponent<HoldablePhysicsHelper>();
		}

		private void Start()
		{
			currentAmmo = maxAmmo;
		}

		private void Update()
		{
			if (currentAmmo < maxAmmo)
			{
				currentAmmo += refillRate * Time.deltaTime;
				currentAmmo = Mathf.Max(currentAmmo, maxAmmo);
			}
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
			if (currentAmmo > 0)
			{
				currentAmmo -= consumeRate * Time.deltaTime;
				Debug.Log("Use");
			}
			// Debounce if run out of ammo
		}

		void IHoldable.ActivateSecondary(HoldableInputPhase phase)
		{
		}

		void IHoldable.BindInstructionOverlay(GameObject spawnedObject)
		{
		}
	}
}