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

		void OnPickup(HoldableHarness harness);
		void OnDrop();

		void ActivatePrimary(InputPhase phase);
		void ActivateSecondary(InputPhase phase);

		GameObject GetInstructionOverlay();
		void BindInstructionOverlay(GameObject spawnedObject);
	}
}