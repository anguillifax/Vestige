using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige.Prototype
{
    public class GreatswordAvatar : MonoBehaviour
    {
		public enum State
		{
			Burning, Extinguished
		}

		[Header("States")]
		public UnityEvent ignited;
		public UnityEvent extinguished;
		public State state;

		[Header("Test")]
		public InspectorCallbackButton testIgnite = new InspectorCallbackButton("Ignite");
		public InspectorCallbackButton testExtinquish = new InspectorCallbackButton("Extinguish");

		// Start is called before the first frame update
		void Awake()
        {
			testIgnite.callback = Ignite;
			testExtinquish.callback = Extinguish;
			state = State.Extinguished;
        }

		public void Ignite()
		{
			switch (state)
			{
				case State.Burning:
					//doesn't do anything
					break;
				case State.Extinguished:
					ignited.Invoke();
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
					state = State.Extinguished;
					break;
				case State.Extinguished:
					//do nothing
					break;
			}

		}
    }
}
