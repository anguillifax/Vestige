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

		[Header("Debug")]
		[SerializeField] private bool debugLogInfo;

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
			if (debugLogInfo)
			{
				Debug.Log($"Dropped holdable {target.Object.name}", target.Object);
			}
			target = null;
		}
	}
}