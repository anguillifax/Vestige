﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vestige
{
	public class MainMenu : MonoBehaviour
	{
		public string scenePlay = "Main";

		public void PlayGame()
		{
			SceneManager.LoadScene(scenePlay);
		}

		public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
			return;
#else
			Application.Quit();
#endif
		}
	}
}