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
		public SystemicEffectTemplate effectTemplate;

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
			overlay = holdable.InstructionOverlay.GetComponent<FlamethrowerOverlay>();
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
			if (holdable.IsHeld) UpdateHoldable();

			if (currentAmmo < maxAmmo && !inUse)
			{
				currentAmmo += refillRate * Time.deltaTime;
				currentAmmo = Mathf.Min(currentAmmo, maxAmmo);
			}
		}

		private void UpdateHoldable()
		{
			bool fire = holdable.InputState.Primary && currentAmmo > 0;
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

		// =========================================================
		// Physic Callbacks
		// =========================================================

		private void OnTriggerStay(Collider other)
		{
			if (inUse && other.attachedRigidbody != null)
			{
				var recipient = other.attachedRigidbody.GetComponent<IRecipient>();
				if (recipient != null)
				{
					Effect e = effectTemplate.AsEffect(gameObject);
					recipient.RecieveEffect(e);
				}
			}
		}
	}
}