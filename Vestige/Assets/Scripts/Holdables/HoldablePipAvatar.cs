using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vestige
{
	public class HoldablePipAvatar : MonoBehaviour
	{
		[Header("Common")]
		public Canvas canvas;
		public Animator anim;
		public Image uiImage;

		private void Awake()
		{
			canvas.worldCamera = Camera.main;
		}

		private void LateUpdate()
		{
			transform.rotation = Quaternion.identity;
		}

		// =========================================================
		// Public Interface
		// =========================================================

		public void SetOpen(bool shown)
		{
			anim.SetBool("Visible", shown);
		}

		public void SetVisible(bool shown)
		{
			uiImage.enabled = shown;
		}
	}
}