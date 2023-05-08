using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class StandardSlashEffectAvatar : MonoBehaviour
	{
		[Header("Common")]
		public UnityEvent started;

		[Header("Debug")]
		public InspectorCallbackButton testBegin = new InspectorCallbackButton("Begin Slash");

		private void Awake()
		{
			testBegin.callback = BeginSlash;
		}

		public void BeginSlash()
		{
			started.Invoke();
		}
	}
}