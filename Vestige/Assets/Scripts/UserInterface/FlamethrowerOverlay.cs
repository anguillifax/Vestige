using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Vestige
{
	public class FlamethrowerOverlay : MonoBehaviour
	{
		public TextMeshProUGUI textMesh;

		public void SetAmmo(float amount)
		{
			textMesh.text = amount.ToString("0.0");
		}
	}
}