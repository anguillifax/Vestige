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

		[HideInInspector] public HoldableHarness harness;
		[HideInInspector] public GameObject overlay;
		public HoldableInputState input;

		// =========================================================
		// Properties
		// =========================================================

		public bool Active => harness != null;

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

		HoldableHarness IHoldable.Harness => harness;
		HoldableConfig IHoldable.Config => config;
		HoldableInputState IHoldable.InputState => input;

		bool IHoldable.Attachable => attachable;

		void IHoldable.OnPickup(HoldableHarness harness)
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

		void IHoldable.OnDrop()
		{
			detached.Invoke();
			physicsHelper.Detach(harness.DropPoint);
			if (harness.overlayContainer && harness.overlayContainer.childCount > 0)
			{
				Destroy(harness.overlayContainer.GetChild(0).gameObject);
			}
			harness = null;
			input = null;
			overlay = null;
		}
	}
}