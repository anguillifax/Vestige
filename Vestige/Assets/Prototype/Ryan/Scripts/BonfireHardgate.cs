using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class BonfireHardgate : MonoBehaviour
	{
		public CampfireController campfire;

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.tag == "Player")
			{
				campfire.Ignite();
			}
		}
	}
}
