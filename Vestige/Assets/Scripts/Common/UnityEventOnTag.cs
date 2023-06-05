using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class UnityEventOnTag : MonoBehaviour
	{
		public string tagName;
		public bool onlyTriggerOnce;
		public UnityEvent callback;

		private bool triggered;

		private void OnTriggerEnter(Collider other)
		{
			if (onlyTriggerOnce && triggered)
			{
				return;
			}

			bool onRoot = other.attachedRigidbody && other.attachedRigidbody.CompareTag(tagName);
			bool onStandalone = other.CompareTag(tagName);
			if (onRoot || onStandalone)
			{
				callback.Invoke();
				triggered = true;
			}
		}
	}
}