using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vestige
{
	public class FinishGameMenu : MonoBehaviour
	{
		public string sceneTitle;

		public void LoadTitle()
		{
			SceneManager.LoadScene(sceneTitle);
		}
	}
}