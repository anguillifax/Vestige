using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Vestige
{
	public class CutsceneTrigger : MonoBehaviour
	{
		[Header("Main")]
		public PlayableDirector director;
		public bool canPlay = true;

		[Header("Debug")]
		public bool logStart;

		private void OnTriggerEnter(Collider other)
		{
			if (canPlay && other.GetComponent<PlayerController>())
			{
				director.Play();
				if (logStart)
				{
					Debug.Log($"Started Cutscene on `{director.gameObject.name}` from trigger", gameObject);
				}
				canPlay = false;
			}
		}
	}
}