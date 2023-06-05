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

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
			if (elevator.GetPlayerIsRiding() == true)
			{
				gateAnimation.enabled = true;
				//gateAnimation.Play("Door animation");
			}
			else{

			}
        }
    }
}
