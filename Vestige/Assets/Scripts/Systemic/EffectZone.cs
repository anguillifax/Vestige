using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class EffectZone : MonoBehaviour
	{
		// =========================================================
		// Variables
		// =========================================================

		[Header("Common")]
		[Expandable] public SystemicEffectTemplate effect;

		// =========================================================
		// Physics Callbacks
		// =========================================================

		private void OnTriggerStay(Collider other)
		{
			if (other.attachedRigidbody)
			{
				ApplyEffect(other.attachedRigidbody.gameObject);
			}
		}

		private void ApplyEffect(GameObject root)
		{
			IRecipient systemic = root.GetComponent<IRecipient>();
			if (systemic != null)
			{
				systemic.RecieveEffect(effect.AsEffect(gameObject));
			}
		}
	}
}