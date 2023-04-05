using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	[Serializable]
	public class InspectorCallbackButton
	{
		public string tooltip;
		public Action callback;

		public InspectorCallbackButton(string tooltip, Action callback = null)
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