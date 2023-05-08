using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vestige
{
	public class PlayerHealthbarAvatar : MonoBehaviour
	{
		[Header("References")]
		public Slider fireSlider;
		public Slider waterSlider;

		private void Start()
		{
			SetFireCapacity(0);
			SetWaterCapacity(0);
		}

		public void SetFireCapacity(float normalized)
		{
			fireSlider.value = normalized;
		}

		public void SetWaterCapacity(float normalized)
		{
			waterSlider.value = normalized;
		}
	}
}