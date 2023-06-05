using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vestige.Prototype
{
    public class Reset : MonoBehaviour
    {
        void Update()
        {
			if (Input.GetKeyDown(KeyCode.P))
			{
				SceneManager.LoadScene(0);
			}
        }
    }
}
