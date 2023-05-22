using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class WaterBuoyancy : MonoBehaviour
	{
		[Header("Common")]
		public float upForce = 10;
		public float forwardForce = 0;
		public float waterSurface = 2;
		public float maxForceDepth = 2;

		private void OnTriggerStay(Collider other)
		{
			if (other.attachedRigidbody)
			{
				Vector3 fwd = forwardForce * transform.TransformDirection(Vector3.forward);
				float upCur = Mathf.Lerp(0, upForce, (transform.position.y - other.attachedRigidbody.position.y) / maxForceDepth);
				Vector3 up = upCur * Vector3.up;
				other.attachedRigidbody.AddForce(fwd + up, ForceMode.VelocityChange);
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawCube(transform.position + new Vector3(0, waterSurface, 0), new Vector3(1, 0.1f, 1));
		}
	}
}