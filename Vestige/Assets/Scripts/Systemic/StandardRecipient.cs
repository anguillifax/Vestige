using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige
{
	public class StandardRecipient : MonoBehaviour, IRecipient
	{
		[Header("Debug")]
		public bool logEffects;

		public EffectCache effects = new EffectCache();

		private void FixedUpdate()
		{
			UpdateData();
		}

		private void UpdateData()
		{
			if (logEffects && effects.Count > 0)
			{
				string contents = string.Join("\n\n", effects.Select(x => x.ToStringVerbose()));
				Debug.Log($"Received Effects on `{gameObject.name}`:\n\n{contents}", this);
			}
			effects.Flip();
		}

		void IRecipient.RecieveEffect(Effect effect)
		{
			effects.Add(effect);
		}
	}
}