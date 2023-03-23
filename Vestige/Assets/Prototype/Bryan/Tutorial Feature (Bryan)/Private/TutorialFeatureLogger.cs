using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
	public class TutorialFeatureLogger : MonoBehaviour
	{
		private void OnEnable()
		{
			Debug.Log($"Tutorial Logger from `{gameObject.name}`", this);
		}
	}
}
