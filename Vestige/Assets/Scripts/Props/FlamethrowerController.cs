using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige
{
	public class FlamethrowerController : MonoBehaviour
	{
		// =========================================================
		// Data
		// =========================================================

		public enum State
		{
			Idle, Firing,
		}

		[Header("Common")]
		public float maxAmmo = 20;
		public float refillRate = 6;
		public float consumeRate = 3;
		public SystemicEffectTemplate effectTemplate;

		private FlamethrowerAvatar avatar;
		private StandardHoldable holdable;
		private FlamethrowerOverlay overlay;
		private StandardRecipient systemic;

		private State state;
		private float curAmmo;

		public float CurAmmo
		{
			get => curAmmo;
			set => curAmmo = Mathf.Clamp(value, 0, maxAmmo);
		}

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			avatar = GetComponent<FlamethrowerAvatar>();
			holdable = GetComponent<StandardHoldable>();
			systemic = GetComponent<StandardRecipient>();

			holdable.attached.AddListener(OnAttach);
			holdable.detached.AddListener(OnDetach);
		}

		private void Start()
		{
			CurAmmo = maxAmmo;
			state = State.Idle;
		}

		private void OnAttach()
		{
			overlay = holdable.InstructionOverlay.GetComponent<FlamethrowerOverlay>();
			state = State.Idle;
		}

		private void OnDetach()
		{
			overlay = null;
			state = State.Idle;
			avatar.StopFiring();
		}

		// =========================================================
		// Logic Update
		// =========================================================

		private void Update()
		{
			if (holdable.IsHeld) UpdateHoldable();
			UpdateCommon();
		}

		private void UpdateHoldable()
		{
			bool hasSoak = systemic.effects.Any(x => x.soak);
			if (hasSoak)
			{
				overlay.SetWaterlogged();
			}
			else
			{
				overlay.SetDried();
			}

			switch (state)
			{
				case State.Idle:
					if (!hasSoak && holdable.InputState.Primary)
					{
						avatar.StartFiring();
						state = State.Firing;
					}
					break;

				case State.Firing:
					CurAmmo -= consumeRate * Time.deltaTime;
					if (hasSoak || !holdable.InputState.Primary)
					{
						avatar.StopFiring();
						state = State.Idle;
					}
					break;
			}

			overlay.SetAmmo(CurAmmo);
		}

		private void UpdateCommon()
		{
			switch (state)
			{
				case State.Idle:
					CurAmmo += refillRate * Time.deltaTime;
					break;

				case State.Firing:
					break;
			}
		}

		// =========================================================
		// Physic Callbacks
		// =========================================================

		private void OnTriggerStay(Collider other)
		{
			if (state == State.Firing)
			{
				SystemicUtil.BroadcastToRigidbody(effectTemplate, gameObject, other);
			}
		}
	}
}