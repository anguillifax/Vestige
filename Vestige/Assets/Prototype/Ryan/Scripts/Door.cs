using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Vestige.Prototype
{
    public class Door : MonoBehaviour
    {
		private bool played = false;

		public int currentSwitches;
		public int listenSwitches;
		public PlayableDirector director;
		public GameObject enemiesGate;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
			if (currentSwitches == listenSwitches && played == false)
			{
				played = true;
				//player animator of door opening 
				director.Play();
				enemiesGate.SetActive(true);

				
			}
        }
    }
}
