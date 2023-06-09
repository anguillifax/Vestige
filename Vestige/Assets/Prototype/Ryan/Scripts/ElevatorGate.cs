using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Vestige.Prototype
{
    public class ElevatorGate : MonoBehaviour
    {
		public Animator gateAnimation;
		public Elevator elevator;

        // Update is called once per frame
        void Update()
        {
			if (elevator.GetPlayerIsRiding() == true)
			{
				gateAnimation.enabled = true;
			}
        }
    }
}
