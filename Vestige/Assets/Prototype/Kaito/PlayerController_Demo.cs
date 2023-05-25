using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if false
namespace Vestige.Prototype
{
// The part of PlayerController that should change if elevator needs to make movements
// other than straight up or straight down.
    public class PlayerController_Demo
    {
		// new members needed
		private bool isOnElevator => elevator != null;
		private Rigidbody elevator;

		private void UpdateMovement()
		{
			Vector3 vel = avatar.Rigidbody.velocity;
			vel.y = 0;

			Vector3 inputs = new Vector3(
				Input.GetAxisRaw("Horizontal"),
				0,
				Input.GetAxisRaw("Vertical"));
			inputs = Vector3.ClampMagnitude(inputs, 1);

			Vector3 rotated = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0) * inputs;

			// the part that needs to change
			if (isOnElevator)
			{
				vel = Vector3.MoveTowards(
					vel,
					(rotated * config.walkVel) + elevator.velocity,
					config.walkAccel * Time.fixedDeltaTime);
			}
			else
			{
				vel = Vector3.MoveTowards(
					vel,
					rotated * config.walkVel,
					config.walkAccel * Time.fixedDeltaTime);
			}

			vel.y = avatar.Rigidbody.velocity.y;

			Vector3 extraGravity = Mathf.Max(0, config.gravityMultiplier - 1) * Physics.gravity;
			vel.y += extraGravity.y * Time.fixedDeltaTime;

			avatar.Rigidbody.velocity = vel;
		}

		// new methods needed
		public void OnElevator(Rigidbody elevator)
		{
			this.elevator = elevator;
		}
		public void OffElevator()
		{
			this.elevator = null;
		}
	}
}
#endif