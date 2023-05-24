using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
    public class RigidbodyDetector : MonoBehaviour
    {
		// enum to specify if the trigger should detect a rigidbody entering or exiting
		private enum DetectMode { Enter, Exit }
		[SerializeField] private DetectMode detectMode;

		// Helper function to check if collider is something that should be detected
		private bool CheckValidCollider(Collider other)
		{
			// objects without rigidbodies are always floating in air, should not be pushed by elevator
			if (other.attachedRigidbody == null)
				return false;

			// don't detect elevators
			if (other.TryGetComponent<Elevator>(out Elevator self))
				return false;

			// also don't detect PlayerController
			if (other.TryGetComponent<PlayerController>(out PlayerController player))
				return false;

			// also don't detect triggers like pickup radius, hitboxes, etc
			if (other.isTrigger)
				return false;

			return true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (detectMode != DetectMode.Enter)
				return;

			if (!CheckValidCollider(other))
				return;

			if (other.attachedRigidbody.useGravity)
			{
				other.attachedRigidbody.transform.SetParent(transform.parent, true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (detectMode != DetectMode.Exit)
				return;

			if (!CheckValidCollider(other))
				return;

			if (other.attachedRigidbody.useGravity)
			{
				other.attachedRigidbody.transform.SetParent(null, true);
			}
		}
	}
}
