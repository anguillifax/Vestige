using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class WaterBombExplosion : MonoBehaviour
    {
		public float duration = 5;

        void Start()
        {
			Destroy(gameObject, duration);
        }
    }
}
