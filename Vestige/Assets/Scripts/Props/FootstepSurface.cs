using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class FootstepSurface : MonoBehaviour
	{
		public enum SurfaceType
		{
			Silent = 0,
			Stone = 1,
			Wood = 2,
			Grass = 3,
			Puddle = 4,
		}

		public SurfaceType type;
	}
}