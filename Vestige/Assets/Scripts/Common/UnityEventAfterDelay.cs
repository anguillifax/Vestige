using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class UnityEventAfterDelay : MonoBehaviour
	{
		public float delay = 2;
		public UnityEvent callback;

		private void Start()
		{
			Invoke(nameof(Call), delay);
		}

		private void Call() => callback.Invoke();
	}
}