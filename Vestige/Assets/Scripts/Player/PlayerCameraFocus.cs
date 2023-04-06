using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class PlayerCameraFocus : MonoBehaviour
	{
		public PlayerController player;

		private void Update()
		{
			transform.position = player.GetCameraFocusPoint();
		}
	}
}