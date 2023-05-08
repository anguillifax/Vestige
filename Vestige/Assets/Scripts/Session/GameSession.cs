using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vestige
{
	public class GameSession : MonoBehaviour
	{
		public void ReloadSceneAfterDelay()
		{
			Invoke(nameof(ReloadInner), 3);
		}

		private void ReloadInner()
		{
			string scene = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene(scene);
		}
	}
}