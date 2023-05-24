using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
    public class Elevator : MonoBehaviour
    {
        // start and end positions
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

		[Tooltip("Speed is used to calculate the time it takes to the other end.\nChanging the start/end points will change speed.")]
        [SerializeField] private float speed;

		// references
		private Rigidbody rb;
        private Transform previous;
        private Transform target;

		// movement
		private float timeToOtherEnd;
		private float timePassed = 0f;
		private float percentagePassed => timePassed / timeToOtherEnd;

		private bool playerIsRiding = false;

		private void Awake()
		{
			rb = GetComponent<Rigidbody>();
			rb.interpolation = RigidbodyInterpolation.Interpolate;

			previous = start;
            target = end;
			timeToOtherEnd = Vector3.Distance(start.position, end.position) / speed;

			SnapToTransforms();
		}

		// Automatically snap to either start or end, based on whichever is closer
		private void SnapToTransforms()
		{
			float distSqrToStart = (transform.position - start.position).sqrMagnitude;
			float distSqrToEnd = (transform.position - end.position).sqrMagnitude;

			bool startIsCloser = distSqrToStart < distSqrToEnd;
			transform.position = (startIsCloser ? start.position : end.position);

			// Set target & previous positions based on which of start/end is closer
			target = (startIsCloser ? end : start);
			previous = (startIsCloser ? start : end);
		}

		public void OnPlayerEntered()
		{
			playerIsRiding = true;
		}

		private void FixedUpdate()
		{
			if (!playerIsRiding)
				return;

			timePassed += Time.fixedDeltaTime;

			rb.MovePosition(Vector3.Lerp(previous.position, target.position, percentagePassed));

			if (percentagePassed >= 1f)
				ChangeTarget();
		}

		// Change target position to previous start position when platform has reached target
		private void ChangeTarget()
		{
			Transform temp = target;
			target = previous;
			previous = temp;

			timePassed = 0f;

			// disable riding until it is triggered again
			playerIsRiding = false;
		}

		public void OnPlayerExited()
		{
		}

	}
}
