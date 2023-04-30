using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Vestige
{
	public class Effect
	{
		// Management
		public GameObject source;
		public IRecipient callback;

		// Attributes
		public bool burn;
		public bool ignite;
		public bool soak;
		public bool douse;
		public bool obliterate;
		public bool regenerate;

		// Negotiations
		public int requestWater;

		public Effect(GameObject source)
		{
			this.source = source;
		}

		public string ToStringVerbose()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Source: ").AppendLine(source ? source.name : "(destroyed)");
			sb.Append("Callback: ").AppendLine(callback == null ? "none" : "present");

			sb.Append("Attributes:");
			if (burn) sb.Append(" Burn");
			if (douse) sb.Append(" Douse");
			if (ignite) sb.Append(" Ignite");
			if (obliterate) sb.Append(" Obliterate");
			if (regenerate) sb.Append(" Regenerate");
			if (soak) sb.Append(" Soak");
			sb.AppendLine();

			sb.AppendLine("Negotiations");
			if (requestWater != 0) sb.Append("requestWater: ").Append(requestWater).AppendLine();

			return sb.ToString();
		}
	}
}