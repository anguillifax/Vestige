using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Vestige.Prototype
{
    public class EndingCutscene : MonoBehaviour
    {
		public PlayableDirector director;

		void OnEnable()
		{
			director.stopped += OnPlayableDirectorStopped;
		}

		void OnPlayableDirectorStopped(PlayableDirector aDirector)
		{
			if (director == aDirector)
				SceneManager.LoadScene("GameComplete");
		}

		void OnDisable()
		{
			director.stopped -= OnPlayableDirectorStopped;
		}


	}
}
