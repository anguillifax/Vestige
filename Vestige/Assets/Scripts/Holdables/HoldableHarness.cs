using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class HoldableHarness : MonoBehaviour
	{
		// =========================================================
		// Support Types
		// =========================================================

		private class AutoKey
		{
			private bool last;

			public void Send(bool current, Action<IHoldable.InputPhase> callback)
			{
				if (!last && current)
				{
					callback(IHoldable.InputPhase.Start);
				}
				else if (last && current)
				{
					callback(IHoldable.InputPhase.Hold);
				}
				else if (last && !current)
				{
					callback(IHoldable.InputPhase.Stop);
				}

				last = current;
			}

			public void Reset()
			{
				last = false;
			}
		}

		// =========================================================
		// Data
		// =========================================================

		[Header("Common")]
		[SerializeField] private IHoldable target;
		[SerializeField] private GameObject owner;
		[SerializeField] private Transform socket;
		[SerializeField] private Transform dropPoint;

		[Header("Interface Bindings")]
		public RectTransform overlayContainer;

		[Header("Debug")]
		[SerializeField] private bool debugLogInfo;

		private readonly AutoKey autoPrimary = new AutoKey();
		private readonly AutoKey autoSecondary = new AutoKey();

		// =========================================================
		// Interface
		// =========================================================

		public IHoldable Target => target;
		public Transform Socket => socket;
		public Transform DropPoint => dropPoint;
		public GameObject Owner => owner;

		public void Attach(IHoldable newTarget)
		{
			if (newTarget == null)
			{
				Debug.LogWarning("Cannot attach null holdable", owner);
				return;
			}

			if (target == newTarget)
			{
				return;
			}

			if (target != null)
			{
				if (debugLogInfo)
				{
					Debug.Log("Replacing with new target");
				}
				Detach();
			}

			target = newTarget;
			target.OnPickup(this);
			if (debugLogInfo)
			{
				Debug.Log($"Picked up holdable {target.Object.name}", target.Object);
			}

			if (overlayContainer != null && target.Config.hudOverlay != null)
			{
				var ui = Instantiate(target.Config.hudOverlay, overlayContainer);
				target.BindInstructionOverlay(ui);
			}

			autoPrimary.Reset();
			autoSecondary.Reset();
		}

		public void Detach()
		{
			if (target == null)
			{
				return;
			}

			target.OnDrop();
			if (debugLogInfo)
			{
				Debug.Log($"Dropped holdable {target.Object.name}", target.Object);
			}
			target = null;

			if (overlayContainer != null)
			{
				Destroy(overlayContainer.GetChild(0).gameObject);
			}
		}

		public void SendPrimaryActionAuto(bool currentValue)
		{
			if (Target != null)
			{
				autoPrimary.Send(currentValue, Target.ActivatePrimary);
			}
		}

		public void SendSecondaryActionAuto(bool currentValue)
		{
			if (Target != null)
			{
				autoSecondary.Send(currentValue, Target.ActivateSecondary);
			}
		}
	}
}