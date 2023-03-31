using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class PlayerController : MonoBehaviour
	{
		[Header("Common")]
		public Transform lookRotation;

		[Header("Walk")]
		public float walkVel = 8;
		public float walkAccel = 30;

		private Rigidbody body;

		private void Awake()
		{
			body = GetComponent<Rigidbody>();

			if (!lookRotation)
			{
				lookRotation = transform;
			}
		}

		private void FixedUpdate()
		{
			Vector3 vel = body.velocity;
			vel.y = 0;

			Vector3 inputs = new Vector3(
				Input.GetAxisRaw("Horizontal"),
				0,
				Input.GetAxisRaw("Vertical"));
			inputs = Vector3.ClampMagnitude(inputs, 1);

			Vector3 rotated = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0) * inputs;

			vel = Vector3.MoveTowards(
				vel,
				rotated * walkVel,
				walkAccel * Time.fixedDeltaTime);

			vel.y = body.velocity.y;
			body.velocity = vel;
		}
	}
}