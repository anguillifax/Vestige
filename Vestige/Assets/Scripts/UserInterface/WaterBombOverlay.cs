using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class WaterBombOverlay : MonoBehaviour
	{
		public UnityEvent activated;

		public void SetHasActivated()
		{
			activated.Invoke();
		}
	}
}