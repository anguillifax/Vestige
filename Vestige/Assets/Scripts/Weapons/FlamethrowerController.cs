using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class FlamethrowerController : MonoBehaviour
	{
		public FlamethrowerAvatar avatar;
		public GameObject fireZone;

		private void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				StartFiring();
			}

			if (Input.GetButtonUp("Fire1"))
			{
				StopFiring();
			}
		}

		public void StartFiring()
		{
			avatar.StartFiring();
			fireZone.SetActive(true);
		}

		public void StopFiring()
		{
			avatar.StopFiring();
			fireZone.SetActive(false);
		}

		private void OnTriggerStay(Collider other)
		{
			Debug.Log("Hit " + other.name, other);
		}
	}
}