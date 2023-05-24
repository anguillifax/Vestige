using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public interface IHoldable
	{
		HoldableHarness Harness { get; }
		HoldableConfig Config { get; }
		HoldableInputState InputState { get; }
		bool Attachable { get; }
		GameObject Root { get; }
		bool IsHeld { get; }
		GameObject InstructionOverlay { get; }

		void OnPickup(HoldableHarness harness);
		void OnDrop();
	}
}