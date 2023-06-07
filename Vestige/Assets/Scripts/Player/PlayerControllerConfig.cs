using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	[CreateAssetMenu(menuName = "Vestige/Player Controller Config", order = 100)]
	public class PlayerControllerConfig : ScriptableObject
	{
		[Serializable]
		public class HealthConfig
		{
			public float max = 100;
			public float depleteRate = 20;
			public float regenerateRate = 12;
		}

		[Header("Camera")]
		public float cameraMouseVertWorldOffset = 3;

		[Header("Health")]
		public HealthConfig healthFire;
		public HealthConfig healthWater;
		public float healthRegenHurt;
		public int regenBonfireLayer;

		[Header("Physics")]
		public float gravityMultiplier = 2;

		[Header("Walk")]
		public float walkVel = 8;
		public float walkAccel = 30;

		[Header("Holdable")]
		public float pickupRadius = 2;
		public Vector3 pickupOffset = new Vector3(0, 0.5f, 0);
		public LayerMask pickupMask = int.MaxValue;
	}
}