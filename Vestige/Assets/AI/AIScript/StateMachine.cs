using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public abstract class StateMachine : MonoBehaviour
	{

		public abstract StateMachine Tick(EnemyManager enemyManger,
		EnemyHealth enemyStats,
		EnemyAnim enemyAnimator,
		EnemyLocomotion enemyLocomotion);


	}
}