using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class DestroyAfterTime : MonoBehaviour
	{
		public float time = 2;

		private void Start()
		{
			Destroy(gameObject, time);
		}
	}
}