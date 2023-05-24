using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class WaterBombSimpleAvatar : MonoBehaviour
	{
		// =========================================================
		// Variables
		// =========================================================

		[Header("Events")]
		public UnityEvent activated;
		public UnityEvent detonated;

		[Header("Debug")]
		public InspectorCallbackButton testActivate = new InspectorCallbackButton("Activate");
		public InspectorCallbackButton testDetonate = new InspectorCallbackButton("Detonate");

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			testActivate.callback = Activate;
			testDetonate.callback = Detonate;
		}

		// =========================================================
		// Public Interface
		// =========================================================

		public void Activate()
		{
			activated.Invoke();
		}

		public void Detonate()
		{
			detonated.Invoke();
		}

		public void DestroyObject()
		{
			Destroy(gameObject, 0.1f);
		}
	}
}