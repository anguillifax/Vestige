using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	[CreateAssetMenu(menuName = "Vestige/Systemic Effect Template", order = 100)]
	public class SystemicEffectTemplate : ScriptableObject
	{
		[Header("Flags")]
		public bool burn;
		public bool ignite;
		public bool soak;
		public bool douse;
		public bool obliterate;
		public bool regenerate;

		public Effect AsEffect(GameObject owner)
		{
			Effect e = new Effect(owner)
			{
				burn = burn,
				ignite = ignite,
				soak = soak,
				douse = douse,
				obliterate = obliterate,
				regenerate = regenerate,
			};

			return e;
		}
	}
}