using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
    public class ExplosiveBarrelAvatar : MonoBehaviour
    {
		[SerializeField]
		protected GameObject explosion;
		
		[SerializeField]
		protected float explosionTimer;

		[Header("Events")]
		public UnityEvent activated;

		[Header("Debug")]
		public InspectorCallbackButton testActivate = new InspectorCallbackButton("Activate");
		public InspectorCallbackButton testExplode = new InspectorCallbackButton("Explode");

		void Awake()
		{
			testActivate.callback = Activate;
			testExplode.callback = Explode;
		}


		public void Activate()
		{
			activated.Invoke();
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
