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
		public GameObject owner;
		public Transform socket;
		public Transform dropPoint;

		[Header("Debug")]
		[SerializeField] private bool debugLogInfo;

		// =========================================================
		// Interface
		// =========================================================

		public IHoldable Target => target;

		public Quaternion InitialRotation => socket.transform.rotation;

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

			target.Object.transform.SetParent(socket);
			MoveTarget(socket.position);

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

			target.Object.transform.SetParent(null);
			MoveTarget(dropPoint.position);
			target.OnDrop();
			if (debugLogInfo)
			{
				Debug.Log($"Dropped holdable {target.Object.name}", target.Object);
			}
			target = null;
		}

		// =========================================================
		// Helper
		// =========================================================

		private void MoveTarget(Vector3 pos)
		{
			var rb = target.Object.GetComponent<Rigidbody>();
			if (rb)
			{
				rb.MovePosition(pos);
			}
			target.Object.transform.position = pos;
		}
	}
}