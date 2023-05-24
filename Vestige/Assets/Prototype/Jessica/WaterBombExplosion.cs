using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige.Prototype
{
    public class WaterBombExplosion : MonoBehaviour
    {
        public float duration = 3;
        
        [Header("Events")]
		public UnityEvent detonated;

        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, duration);
        }
    }
}
