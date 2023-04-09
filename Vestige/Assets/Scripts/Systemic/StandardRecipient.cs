using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class StandardRecipient : MonoBehaviour, IRecipient
	{
		public EffectCache effects = new EffectCache();

		private void LateUpdate()
		{
			effects.Flip();
		}

		void IRecipient.RecieveEffect(Effect effect)
		{
			effects.Add(effect);
		}
	}
}