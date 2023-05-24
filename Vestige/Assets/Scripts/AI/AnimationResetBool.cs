using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class AnimationResetBool : StateMachineBehaviour
	{
		public string targetAnimation;

		public bool status;
		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			animator.SetBool(targetAnimation, status);

		}
	}
}