using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class FlamethrowerAvatar : MonoBehaviour
	{
		[Header("Common")]
		public UnityEvent started;
		public UnityEvent stopped;

		[Header("Test Buttons")]
		public InspectorCallbackButton testStart = new InspectorCallbackButton("Start Firing");
		public InspectorCallbackButton testStop = new InspectorCallbackButton("Stop Firing");

		private void Awake()
		{
			testStart.callback = StartFiring;
			testStop.callback = StopFiring;
			stopped.Invoke();
		}

		public void StartFiring()
		{
			started.Invoke();
		}

		public void StopFiring()
		{
			stopped.Invoke();
		}
	}
}