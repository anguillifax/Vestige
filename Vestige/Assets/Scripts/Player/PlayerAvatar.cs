using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige
{
	public class PlayerAvatar : MonoBehaviour
	{
		// =========================================================
		// Types
		// =========================================================

		public enum HurtState
		{
			Idle, Hurt,
		}

		// =========================================================
		// Variables
		// =========================================================

		[Header("Model")]
		public Transform modelRoot;
		public float modelRotateSpeed = 30;
		
		[Header("Hurt Effect")]
		public UnityEvent hurtStarted;
		public UnityEvent hurtStopped;

		[Header("Death")]
		public UnityEvent died;

		private HurtState hurtCur;
		private Animator anim;
		private bool hasDied;
		private Vector3 curDir;

		// =========================================================
		// Implementation
		// =========================================================

		private void Awake()
		{
			anim = GetComponent<Animator>();

			hurtCur = HurtState.Idle;
			hasDied = false;
		}

		private void Update()
		{
			//modelRoot.rotation = Quaternion.LookRotation(curDir, Vector3.up);
		}

		// =========================================================
		// Public Interface
		// =========================================================

		public void SetWalk(float x, float y)
		{
			curDir = transform.TransformDirection(new Vector3(x, 0, y));
		}

		public void SetHurtEffect(bool active)
		{
			switch (hurtCur)
			{
				case HurtState.Idle:
					if (active)
					{
						hurtStarted.Invoke();
						hurtCur = HurtState.Hurt;
					}
					break;

				case HurtState.Hurt:
					if (!active)
					{
						hurtStopped.Invoke();
						hurtCur = HurtState.Idle;
					}
					break;
			}
		}

		public void StartDeathEffects()
		{
			if (!hasDied)
			{
				hasDied = true;
				died.Invoke();
			}
		}
	}
}