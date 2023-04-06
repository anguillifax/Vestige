using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	[CreateAssetMenu(menuName = "Vestige/Player Controller Config", order = 100)]
	public class PlayerControllerConfig : ScriptableObject
	{
		[Header("Camera")]
		public float cameraMouseVertWorldOffset = 3;

		[Header("Walk")]
		public float walkVel = 8;
		public float walkAccel = 30;
	}
}