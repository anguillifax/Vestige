using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;

namespace Vestige
{
	public class FlamePit : MonoBehaviour
	{
		// =========================================================
		// Types
		// =========================================================

		public enum State
		{
			Burning, Inactive,
		}

		// =========================================================
		// Variables
		// =========================================================

		[Header("Common")]
		public State initialState = State.Burning;

		[Header("Events")]
		public UnityEvent ignited;
		public UnityEvent extinguished;

		private State state;
		private StandardRecipient systemic;

		// =========================================================
		// Behavior
		// =========================================================

		private void Awake()
		{
			systemic = GetComponent<StandardRecipient>();
		}

		private void Start()
		{
			state = initialState;
			if (state == State.Burning) ignited.Invoke();
			if (state == State.Inactive) extinguished.Invoke();
		}

		private void Update()
		{
			switch (state)
			{
				case State.Burning:
					if (systemic.effects.Any(x => x.douse))
					{
						extinguished.Invoke();
						state = State.Inactive;
					}
					break;

				case State.Inactive:
					if (systemic.effects.Any(x => x.ignite))
					{
						ignited.Invoke();
						state = State.Burning;
					}
					break;
			}
		}

		//Adding to turn off gameobject when doused - Ryan
		public State getState()
		{
			return state;
		}
	}
}