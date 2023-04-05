using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	[Serializable]
	public class InspectorCallbackButton
	{
		[SerializeField]
		private string tooltip;
		private readonly Action callback;

		public InspectorCallbackButton(string tooltip, Action callback)
		{
			this.tooltip = tooltip;
			this.callback = callback;
		}

		public void Invoke()
		{
			callback?.Invoke();
		}
	}
}