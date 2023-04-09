using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class CampfireAvatar : MonoBehaviour
	{
		public enum State
		{
			Burning, Extinguished
		}

		[Header("Startup")]
		public bool allowInitialSetup = true;
		public State initialState = State.Burning;
		
		[Header("Events")]
		public UnityEvent ignited;
		public UnityEvent extinguished;

		[Header("Debug")]
		[SerializeField] private InspectorCallbackButton testIgnite = new InspectorCallbackButton("Ignite");
		[SerializeField] private InspectorCallbackButton testExtinguish = new InspectorCallbackButton("Extinguish");

		private void Awake()
		{
			testIgnite.callback = Ignite;
			testExtinguish.callback = Extinguish;
		}

		private void Start()
		{
			if (allowInitialSetup && initialState == State.Burning)
			{
				Ignite();
			}
		}

		public void Ignite()
		{
			ignited.Invoke();
		}

		public void Extinguish()
		{
			extinguished.Invoke();
		}
	}
}