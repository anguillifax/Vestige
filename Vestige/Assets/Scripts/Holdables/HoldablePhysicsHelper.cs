using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	[Serializable]
	public class HoldablePhysicsHelper
	{
		// =========================================================
		// Data
		// =========================================================

		public Transform transform;
		public Collider[] solidColliders;
		public Rigidbody mainRigidbody;

		private Transform origParent;

		// =========================================================
		// Public Interface
		// =========================================================

		public void Attach(Transform socket)
		{
			origParent = transform.parent;
			transform.parent = socket;

			MoveRotate(socket.position, socket.rotation);

			foreach (var c in solidColliders)
			{
				c.enabled = false;
			}

			mainRigidbody.isKinematic = true;
		}

		public void Detach(Transform dropPoint)
		{
			transform.parent = origParent;

			MoveRotate(dropPoint.position, dropPoint.rotation);

			foreach (var c in solidColliders)
			{
				c.enabled = true;
			}

			mainRigidbody.isKinematic = false;
		}

		// =========================================================
		// Helper
		// =========================================================

		private void MoveRotate(Vector3 pos, Quaternion rot)
		{
			mainRigidbody.MovePosition(pos);
			transform.position = pos;

			mainRigidbody.MoveRotation(rot);
			transform.rotation = rot;
		}
	}
}