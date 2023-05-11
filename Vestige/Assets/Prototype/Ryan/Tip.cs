using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Vestige.Prototype
{
    public class Tip : MonoBehaviour
    {

		public string tipMessage = "";
		public TMP_Text tipText;
		public GameObject tipObject;

		private void Start()
		{
			tipObject.SetActive(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			//if player collides with trigger
			if (other.gameObject.GetComponent<PlayerController>())
			{
				//load up message
				tipObject.SetActive(true);
				tipText.text = tipMessage;
			
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.GetComponent<PlayerController>())
			{
				//load up message
				tipObject.SetActive(false);
			}
		}



	}		
}
