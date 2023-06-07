using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vestige
{
	public class HoldablePipController : MonoBehaviour
	{
		private HoldablePipAvatar avatar;

		private void Awake()
		{
			avatar = GetComponent<HoldablePipAvatar>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				avatar.SetOpen(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				avatar.SetOpen(false);
			}
		}

		// =========================================================
		// Public Interface
		// =========================================================

		public void SetShown(bool canShow)
		{
			avatar.SetVisible(canShow);
		}
	}
}