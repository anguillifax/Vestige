using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Vestige
{
	public struct HoldableInputState
	{
		public bool Primary { get; set; }
		public bool PrimaryDown { get; set; }
		public bool PrimaryUp { get; set; }

		public bool Secondary { get; set; }
		public bool SecondaryDown { get; set; }
		public bool SecondaryUp { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("HoldableInputState(Primary[ ");
			if (PrimaryDown) sb.Append("down ");
			if (Primary) sb.Append("hold ");
			if (PrimaryUp) sb.Append("up ");

			sb.Append("], Secondary[ ");
			if (SecondaryDown) sb.Append("down ");
			if (Secondary) sb.Append("hold ");
			if (SecondaryUp) sb.Append("up ");

			sb.Append("])");
			return sb.ToString();
		}
	}
}