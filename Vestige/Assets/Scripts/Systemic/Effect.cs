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
			sb.Append("Source: ").AppendLine(source.name);
			sb.Append("Callback: ").AppendLine(callback == null ? "none" : "present");

			sb.AppendLine("Attributes");
			if (burn) sb.AppendLine("burn");
			if (ignite) sb.AppendLine("ignite");
			if (soak) sb.AppendLine("soak");
			if (douse) sb.AppendLine("douse");
			if (obliterate) sb.AppendLine("obliterate");
			if (regenerate) sb.AppendLine("regenerate");

			sb.AppendLine("Negotiations");
			if (requestWater != 0) sb.Append("requestWater: ").Append(requestWater).AppendLine();

			return sb.ToString();
		}
	}
}