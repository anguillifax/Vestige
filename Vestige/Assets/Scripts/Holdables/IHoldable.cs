using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public interface IHoldable
	{
		public enum InputPhase
		{
			Start, Hold, Stop
		}

		GameObject Object { get; }
		HoldableConfig Config { get; }

		void OnPickup(HoldableHarness harness);
		void OnDrop();

		void ActivatePrimary(InputPhase phase);
		void ActivateSecondary(InputPhase phase);

		void BindInstructionOverlay(GameObject spawnedObject);
	}
}