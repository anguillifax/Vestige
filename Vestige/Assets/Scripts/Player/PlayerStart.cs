using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class PlayerStart : MonoBehaviour
	{
		[Tooltip("Mark true to use this player start if no other player start is assigned in the current game session.")]
		public bool defaultStart;
	}
}