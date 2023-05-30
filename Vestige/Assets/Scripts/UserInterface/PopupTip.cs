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

		[Header("ButtonsUI")]
		public GameObject buttonIcon;
		/*
		public GameObject e;
		public GameObject rightClick;
		public GameObject leftClick;

		private bool eUI = false;
		private bool rightClickUI = false;
		private bool leftClickUI = false;
		*/

		private void Start()
		{
			tipObject.SetActive(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<PlayerController>())
			{
				if(buttonIcon != null)
				{
					buttonIcon.SetActive(true);
				}

				tipObject.SetActive(true);
				tipText.text = tipMessage;

			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.GetComponent<PlayerController>())
			{
				if (buttonIcon != null)
				{
					buttonIcon.SetActive(false);
				}
				tipObject.SetActive(false);
			}
		}
	}
}
