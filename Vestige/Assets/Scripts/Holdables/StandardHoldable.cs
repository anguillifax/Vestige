using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class StandardHoldable : MonoBehaviour, IHoldable
	{
		// =========================================================
		// Data
		// =========================================================

		private static HoldableConfig sharedDefaultConfig;

		[Header("Common")]
		public bool attachable = true;
		[Expandable] public HoldableConfig config;
		public bool autoConfigPhysicsHelper = true;
		public HoldablePhysicsHelper physicsHelper;

		[Header("Events")]
		public UnityEvent attached;
		public UnityEvent detached;

		private HoldableHarness harness;
		private GameObject overlay;
		private HoldableInputState input;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			if (config == null)
			{
				if (sharedDefaultConfig == null)
				{
					sharedDefaultConfig = Resources.Load<HoldableConfig>("Default Holdable Config");
				}
				config = sharedDefaultConfig;
			}

			if (autoConfigPhysicsHelper)
			{
				physicsHelper.transform = transform;
				physicsHelper.mainRigidbody = GetComponent<Rigidbody>();
				physicsHelper.solidColliders = GetComponentsInChildren<Collider>()
					.Where(x => !x.isTrigger)
					.ToArray();
			}

			overlay = null;

			if (harness != null)
			{
				harness.Attach(this);
			}
		}

		// =========================================================
		// IHoldable Implementation
		// =========================================================

		public HoldableHarness Harness => harness;
		public HoldableConfig Config => config;
		public HoldableInputState InputState => input;
		public bool Attachable => attachable;
		public GameObject Root => gameObject;
		public bool IsHeld => harness != null;
		public GameObject InstructionOverlay => overlay;

		public void OnPickup(HoldableHarness harness)
		{
			this.harness = harness;
			physicsHelper.Attach(harness.Socket);
			if (harness.overlayContainer != null && config.hudOverlay != null)
			{
				overlay = Instantiate(config.hudOverlay, harness.overlayContainer);
			}
			input = new HoldableInputState();
			attached.Invoke();
		}

		public void OnDrop()
		{
			detached.Invoke();
			physicsHelper.Detach(harness.DropPoint);
			if (overlay != null)
			{
				Destroy(overlay);
				overlay = null;
			}
			harness = null;
			input = null;
		}
	}
}