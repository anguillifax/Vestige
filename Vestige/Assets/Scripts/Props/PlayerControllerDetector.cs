using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
    [RequireComponent(typeof(Collider))]
    public class PlayerControllerDetector : MonoBehaviour
    {
		// enum to specify if the trigger should detect the player entering or exiting
		private enum DetectMode { Enter, Exit }
		[SerializeField] private DetectMode detectMode;

		// method on Elevator to invoke when triggered
		[SerializeField] private UnityEvent playerDetected;

		private void OnTriggerEnter(Collider other)
		{
			if (detectMode != DetectMode.Enter)
				return;

			if (other.TryGetComponent<PlayerController>(out PlayerController player))
			{
				//player.OnElevator(transform.parent.GetComponent<Rigidbody>());
				playerDetected?.Invoke();
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (detectMode != DetectMode.Exit)
				return;

			if (other.TryGetComponent<PlayerController>(out PlayerController player))
			{
				//player.OffElevator();
				playerDetected?.Invoke();
			}
		}
	}
}
