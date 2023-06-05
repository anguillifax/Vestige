using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class Reset : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
			if (Input.GetKeyDown(KeyCode.P))
			{
				Application.LoadLevel(0);
			}
        }
    }
}
