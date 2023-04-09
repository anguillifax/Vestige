using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class CustomCursorHarness : MonoBehaviour
	{
		private void OnEnable()
		{
			Cursor.visible = false;
		}

		private void Update()
		{
			transform.position = Input.mousePosition;
		}

		private void OnDisable()
		{
			Cursor.visible = true;
		}
	}
}