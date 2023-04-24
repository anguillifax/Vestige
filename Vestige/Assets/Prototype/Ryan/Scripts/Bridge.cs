using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class Bridge : MonoBehaviour
    {

		private void Update()
		{
			if (Input.GetKeyDown("b"))
			{
				Destroy(this.gameObject);
			}
		}
	}
}
