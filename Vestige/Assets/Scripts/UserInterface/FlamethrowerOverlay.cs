using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class FlamethrowerOverlay : MonoBehaviour
	{
		// =========================================================
		// Variables
		// =========================================================

		public enum State
		{
			Normal, Waterlogged,
		}

		public TextMeshProUGUI textMesh;
		public UnityEvent waterlogged;
		public UnityEvent dried;

		private State state;

		public void SetAmmo(float amount)
		{
			textMesh.text = amount.ToString("0.0");
		}

		public void SetWaterlogged()
		{
			switch (state)
			{
				case State.Normal:
					waterlogged.Invoke();
					state = State.Waterlogged;
					break;

				case State.Waterlogged:
					// No Action
					break;
			}
		}

		public void SetDried()
		{
			switch (state)
			{
				case State.Normal:
					// No Action
					break;

				case State.Waterlogged:
					dried.Invoke();
					state = State.Normal;
					break;
			}
		}
	}
}