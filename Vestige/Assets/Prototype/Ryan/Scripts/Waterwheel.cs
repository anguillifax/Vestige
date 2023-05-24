using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class Waterwheel : MonoBehaviour
    {
		[SerializeField]
		private GameObject wheel;

		[SerializeField]
		private float roationSpeed;
		
        // Update is called once per frame
        private void Update()
        {
			wheel.transform.Rotate(roationSpeed * Time.deltaTime,0, 0);
        }
    }
}
