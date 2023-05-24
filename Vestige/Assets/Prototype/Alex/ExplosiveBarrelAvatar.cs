using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class ExplosiveBarrelAvatar : MonoBehaviour
    {
		[SerializeField]
		protected GameObject explosion;
		
		[SerializeField]
		protected float explosionTimer;

		[Header("Debug")]
		[SerializeField] private InspectorCallbackButton testExplode = new InspectorCallbackButton("Explode");
		void Awake()
		{
			testExplode.callback = Explode;
		}

		public void Explode()
		{

			GameObject expl = Instantiate(explosion, transform.position, transform.rotation);
			
			//Destroys barrel object when exploded
			Destroy(gameObject);

			//Makes sure explosion animation plays for a little then destroys the explosion object
			Destroy(expl, explosionTimer);
		}

    }
}
