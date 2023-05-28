using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class Switch : PopupTip
    {
		public Door doorToOpen;
		public GameObject greenLights;

		private void Start()
		{
			greenLights.SetActive(false);
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.gameObject.GetComponent<PlayerController>())
			{
				if (Input.GetKey(KeyCode.F))
				{
					doorToOpen.currentSwitches++;
					greenLights.SetActive(true);
					tipObject.SetActive(false);
					Destroy(gameObject);
				}
			}
		}
		
	}
}
