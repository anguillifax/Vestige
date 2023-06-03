using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class CameraLooker : MonoBehaviour
    {
		public GameObject cameraObject;

		private void OnTriggerStay(Collider other)
		{
			if(other.tag == "Player")
			{
				cameraObject.transform.LookAt(other.transform);
			}
		}
	}
}
