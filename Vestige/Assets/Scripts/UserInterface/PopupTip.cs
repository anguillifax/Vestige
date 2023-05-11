using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Vestige
{
	public class PopupTip : MonoBehaviour
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
			if (other.gameObject.GetComponent<PlayerController>())
			{
				tipObject.SetActive(true);
				tipText.text = tipMessage;

			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.GetComponent<PlayerController>())
			{
				tipObject.SetActive(false);
			}
		}
	}
}
