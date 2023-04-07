using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public interface IHoldable
	{
		GameObject Object { get; }
		HoldableConfig Config { get; }
		HoldableInputState InputState { get; }

		void OnPickup(HoldableHarness harness);
		void OnDrop();
	}
}