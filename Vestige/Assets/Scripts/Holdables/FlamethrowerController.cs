using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class FlamethrowerController : MonoBehaviour, IHoldable
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
		public FlamethrowerOverlay overlay;
		public bool inUse;

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
			if (currentAmmo < maxAmmo && !inUse)
			{
				currentAmmo += refillRate * Time.deltaTime;
				currentAmmo = Mathf.Min(currentAmmo, maxAmmo);
			}

			if (overlay != null)
			{
				overlay.SetAmmo(currentAmmo);
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
			inUse = false;
		}

		void IHoldable.OnDrop()
		{
			physicsHelper.Detach(harness.DropPoint);
			harness = null;
		}

		void IHoldable.ReceiveInput(HoldableInputState input)
		{
			bool fire = input.Primary && currentAmmo > 0;
			if (fire && !inUse)
			{
				avatar.StartFiring();
			}
			if (fire)
			{
				currentAmmo -= consumeRate * Time.deltaTime;
			}
			if (!fire && inUse)
			{
				avatar.StopFiring();
			}
			inUse = fire;
		}

		void IHoldable.BindInstructionOverlay(GameObject spawnedObject)
		{
			overlay = spawnedObject.GetComponent<FlamethrowerOverlay>();
		}
	}
}