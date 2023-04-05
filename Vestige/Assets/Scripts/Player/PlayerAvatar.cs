using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class PlayerAvatar : MonoBehaviour
	{
		public float horzVel;
		public float vertVel;

		public Rigidbody Rigidbody { get; private set; }

		private Animator anim;

		private void Awake()
		{
			Rigidbody = GetComponent<Rigidbody>();
			anim = GetComponent<Animator>();
		}

		private void Update()
		{
			//anim.SetFloat("Vertical", vertVel);
			//anim.SetFloat("Horizontal", horzVel);
		}
	}
}