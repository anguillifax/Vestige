using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class TorchController : MonoBehaviour
	{
		// =========================================================
		// Data
		// =========================================================

		private StandardHoldable holdable;

		// =========================================================
		// Initialization
		// =========================================================

		private void Awake()
		{
			holdable = GetComponent<StandardHoldable>();
		}

		// =========================================================
		// Logic Update
		// =========================================================

		private void Update()
		{
			if (holdable.Active) UpdateHoldable();
		}

		private void UpdateHoldable()
		{
			if (holdable.input.SecondaryDown)
			{
				holdable.harness.Detach();
			}
		}
	}
}