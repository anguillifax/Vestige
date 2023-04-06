using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public interface IHoldable
	{
		GameObject Object { get; }
		HoldableConfig Config { get; }

		void OnPickup(HoldableHarness harness);
		void OnDrop();

		void ActivatePrimary(HoldableInputPhase phase);
		void ActivateSecondary(HoldableInputPhase phase);

		void BindInstructionOverlay(GameObject spawnedObject);
	}
}