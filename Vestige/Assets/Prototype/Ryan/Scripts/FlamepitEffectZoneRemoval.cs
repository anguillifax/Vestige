using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class FlamepitEffectZoneRemoval : MonoBehaviour
    {
		public FlamePit flamePit;

        // Update is called once per frame
        void Update()
        {
			if (flamePit.getState() == FlamePit.State.Inactive)
			{
				flamePit.gameObject.SetActive(false);
			}
		}
    }
}
