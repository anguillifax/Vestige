using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class SpawnDestroyGameObject : MonoBehaviour
	{
		public GameObject template;

		public void DestroySelf(float delay)
		{
			Destroy(gameObject, delay);
		}

		public void InstantiateTemplate()
		{
			Instantiate(template, transform.position, template.transform.rotation);
		}
	}
}