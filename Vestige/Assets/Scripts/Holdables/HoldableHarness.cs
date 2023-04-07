using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class HoldableHarness : MonoBehaviour
	{
		// =========================================================
		// Data
		// =========================================================

		[Header("Common")]
		[SerializeField] private IHoldable target;
		[SerializeField] private GameObject owner;
		[SerializeField] private Transform socket;
		[SerializeField] private Transform dropPoint;

		[Header("Interface Bindings")]
		public RectTransform overlayContainer;

		[Header("Debug")]
		[SerializeField] private bool debugLogInfo;

		private bool primaryLast;
		private bool secondaryLast;

		// =========================================================
		// Interface
		// =========================================================

		public IHoldable Target => target;
		public Transform Socket => socket;
		public Transform DropPoint => dropPoint;
		public GameObject Owner => owner;

		public void Attach(IHoldable newTarget)
		{
			if (newTarget == null)
			{
				Debug.LogWarning("Cannot attach null holdable", owner);
				return;
			}

			if (target == newTarget)
			{
				return;
			}

			if (target != null)
			{
				if (debugLogInfo)
				{
					Debug.Log("Replacing with new target");
				}
				Detach();
			}

			target = newTarget;
			target.OnPickup(this);

			primaryLast = false;
			secondaryLast = false;

			if (debugLogInfo)
			{
				Debug.Log($"Picked up holdable {target.Object.name}", target.Object);
			}
		}

		public void Detach()
		{
			if (target == null)
			{
				return;
			}

			target.OnDrop();
			target = null;

			if (debugLogInfo)
			{
				Debug.Log($"Dropped holdable {target.Object.name}", target.Object);
			}
		}

		public void SendInputs(bool primary, bool secondary)
		{
			if (target == null)
			{
				primaryLast = false;
				secondaryLast = false;
				return;
			}

			target.InputState.SetFromContinuous(primary, primaryLast, secondary, secondaryLast);

			primaryLast = primary;
			secondaryLast = secondary;
		}
	}
}