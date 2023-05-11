using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
	public class HoldableDetach : MonoBehaviour
	{
		
		public void Detach()
		{
			var h = GetComponent<IHoldable>();
			if(h.Harness != null)
			{
				h.Harness.Detach();
			}
		}

    }
}
