using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class FlamethrowerController : MonoBehaviour
	{
		// =========================================================
		// Data
		// =========================================================

		[Header("Common")]
		public float maxAmmo = 20;
		public float refillRate = 6;
		public float consumeRate = 3;

		private FlamethrowerAvatar avatar;
		private StandardHoldable holdable;
		private FlamethrowerOverlay overlay;

		private float currentAmmo;
		private bool inUse;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			avatar = GetComponent<FlamethrowerAvatar>();
			holdable = GetComponent<StandardHoldable>();

			holdable.attached.AddListener(OnAttach);
			holdable.detached.AddListener(OnDetach);
		}

		private void Start()
		{
			currentAmmo = maxAmmo;
		}

		private void OnAttach()
		{
			overlay = holdable.overlay.GetComponent<FlamethrowerOverlay>();
			inUse = false;
		}

		private void OnDetach()
		{
			overlay = null;
			inUse = false;
		}

		// =========================================================
		// Logic Update
		// =========================================================

		private void Update()
		{
			if (holdable.Active) UpdateHoldable();

			if (currentAmmo < maxAmmo && !inUse)
			{
				currentAmmo += refillRate * Time.deltaTime;
				currentAmmo = Mathf.Min(currentAmmo, maxAmmo);
			}
		}

		private void UpdateHoldable()
		{
			bool fire = holdable.input.Primary && currentAmmo > 0;
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

			overlay.SetAmmo(currentAmmo);
		}
	}
}