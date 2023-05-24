using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class StandardThrowable : MonoBehaviour
	{
		// =========================================================
		// Variables
		// =========================================================

		[Header("Common")]
		public bool triggerOnSecondaryAction = true;
		public bool detachFromHoldable = true;

		[Header("Arc")]
		public float throwAngle = 50;
		public float throwVelocity = 6f;
		public float inheritFactor = 0.4f;

		private IHoldable holdable;

		// =========================================================
		// Implementation
		// =========================================================

		private void Awake()
		{
			holdable = GetComponent<IHoldable>();
		}

		private void Update()
		{
			if (triggerOnSecondaryAction && holdable.IsHeld && holdable.InputState.SecondaryDown)
			{
				ThrowObject();
			}
		}

		// =========================================================
		// Public Interface
		// =========================================================

		public void ThrowObject()
		{
			if (holdable == null)
			{
				Debug.LogWarning("Cannot throw object that is not holdable");
				return;
			}

			if (!holdable.IsHeld)
			{
				return;
			}

			Vector3 localDir = Quaternion.Euler(-throwAngle, 0, 0) * Vector3.forward;
			Vector3 outDir = holdable.Root.transform.TransformDirection(localDir);

			Vector3 vel = outDir * throwVelocity;

			Rigidbody ownerBody = holdable.Harness.Owner.GetComponent<Rigidbody>();
			if (ownerBody != null)
			{
				vel += ownerBody.velocity * inheritFactor;
			}

			if (detachFromHoldable)
			{
				holdable.Harness.Detach();
			}

			GetComponent<Rigidbody>().velocity = vel;
		}
	}
}