using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige.Prototype
{
    public class Switch : PopupTip
    {
		public UnityEvent activated;
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
					activated.Invoke();
					Destroy(gameObject);
				}
			}
		}
		
	}
}
