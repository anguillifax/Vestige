using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige
{
	public class EnemyHealth : MonoBehaviour
	{
		// Start is called before the first frame update
		[SerializeField] float MaximumHealth = 100;

		public StandardRecipient systemic;

		float currentHealth;

		void Start()
		{

			currentHealth = MaximumHealth;
		}

		// void Update()
		// {
		// 	float receivedDamage = systemic.effects
		// 		.Sum(x => x.requestWater);
		// 	if (receivedDamage > 0)
		// 	{
		// 		EnemyTakeDamage(receivedDamage);
		// 	}
		// }

		public void EnemyTakeDamage(float Damage)
		{
			currentHealth -= Damage;
		}
		public bool EnemyDead()
		{
			return currentHealth <= 0;
		}

	}
}
