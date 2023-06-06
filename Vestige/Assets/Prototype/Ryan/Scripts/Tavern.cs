using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace Vestige.Prototype
{
	public class Tavern : MonoBehaviour
	{
		public GameObject enemy;
		public GameObject playerController;
		public Transform location;
		private int counter = 0;
		private bool spawn = false;

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<PlayerController>())
			{
				enemy.GetComponent<EnemyManager>().currentTarget = playerController.transform;

				if (counter >= 20)
					spawn = false;


				if (counter <= 20 && spawn)
				{
					counter++;
					Instantiate(enemy, new Vector3(location.position.x, location.position.y, location.position.z), Quaternion.identity);
				}
				else
				{
					counter--;
					if (counter <= 0)
						spawn = true;
				}


			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.GetComponent<PlayerController>())
			{
				enemy.GetComponent<EnemyManager>().currentTarget = playerController.transform;

				if (counter >= 20)
					spawn = false;


				if (counter <= 20 && spawn)
				{
					counter+=5;
					Instantiate(enemy, new Vector3(location.position.x, location.position.y, location.position.z), Quaternion.identity);
				}
				else
				{
					counter--;
					if (counter <= 0)
						spawn = true;
				}


			}
		}
	}
}
