using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Vestige
{
	public class HoldableInputState
	{
		private class Button
		{
			public bool hold;
			public bool down;
			public bool up;

			public void Update(bool current, bool last)
			{
				hold = current;
				down = !last && current;
				up = last && !current;
			}

			public void Clear()
			{
				hold = false;
				down = false;
				up = false;
			}
		}

		public bool Primary => primary.hold;
		public bool PrimaryDown => primary.down;
		public bool PrimaryUp => primary.up;

		public bool Secondary => secondary.hold;
		public bool SecondaryDown => secondary.down;
		public bool SecondaryUp => secondary.up;

		private readonly Button primary = new Button();
		private readonly Button secondary = new Button();

		public void SetFromContinuous(bool curPrimary, bool lastPrimary, bool curSecondary, bool lastSecondary)
		{
			primary.Update(curPrimary, lastPrimary);
			secondary.Update(curSecondary, lastSecondary);
		}

		public void ClearInputs()
		{
			primary.Clear();
			secondary.Clear();
		}

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