using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class HoldableSpawner : MonoBehaviour
    {
		public GameObject holdablePrefab;
		public GameObject holdableInUse;
		public Transform holdableTransform;
	
        // Update is called once per frame
        void Update()
        {
			if(holdableInUse == null)
			{
				//spawn holdable
				holdableInUse = Instantiate(holdablePrefab, holdableTransform);

			}
        }
    }
}
