using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class TorchAvatar : MonoBehaviour
	{
		// =========================================================
		// Variables
		// =========================================================

		public enum State
		{
			Burning, Extinguished,
		}

		[Header("Events")]
		public UnityEvent ignited;
		public UnityEvent extinguished;

		[Header("Debug")]
		public InspectorCallbackButton testIgnite = new InspectorCallbackButton("Ignite");
		public InspectorCallbackButton testExtinguish = new InspectorCallbackButton("Extinguish");

		private State state;

		// =========================================================
		// Update
		// =========================================================

		private void Start()
		{
			ConfigureState(State.Burning);
		}

		// =========================================================
		// Public Interface
		// =========================================================

		public void ConfigureState(State nextState)
		{
			switch (nextState)
			{
				case State.Burning:
					ignited.Invoke();
					state = State.Burning;
					break;

				case State.Extinguished:
					extinguished.Invoke();
					state = State.Extinguished;
					break;
			}
		}

		public void Ignite()
		{
			switch (state)
			{
				case State.Burning:
					// No Action
					break;

				case State.Extinguished:
					ignited.Invoke();
					// TODO Sound
					state = State.Burning;
					break;
			}
		}

		public void Extinguish()
		{
			switch (state)
			{
				case State.Burning:
					extinguished.Invoke();
					// TODO Sound
					state = State.Extinguished;
					break;

				case State.Extinguished:
					// No Action
					break;
			}
		}
	}
}