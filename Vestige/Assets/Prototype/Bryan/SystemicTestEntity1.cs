using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige.Prototype
{
	public class SystemicTestEntity1 : MonoBehaviour, IRecipient
	{
		// =========================================================
		// Representation
		// =========================================================

		public bool logEffectsVerbose;

		private readonly EffectCache effects = new EffectCache();

		// =========================================================
		// Events
		// =========================================================

		private void FixedUpdate()
		{
			effects.Flip();
			if (logEffectsVerbose)
			{
				Debug.Log(
					string.Join("\n\n", effects.Select(x => x.ToStringVerbose())),
					this);
			}
		}

		// =========================================================
		// Interface
		// =========================================================

		public void RecieveEffect(Effect effect)
		{
			effects.Add(effect);
		}
	}
}
