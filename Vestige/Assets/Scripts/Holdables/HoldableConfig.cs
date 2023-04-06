using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	[CreateAssetMenu(menuName = "Vestige/Holdable Config", order = 100)]
	public class HoldableConfig : ScriptableObject
	{
		[Tooltip("Provides instructions on how to use holdable")]
		public GameObject hudOverlay;
	}
}