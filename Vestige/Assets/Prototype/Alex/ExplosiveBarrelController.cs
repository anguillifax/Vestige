using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Vestige.Prototype
{
    public class ExplosiveBarrelController : MonoBehaviour
    {

		public enum State
		{
			Idle, Burning, Exploding
		}

		//Maybe an hp system since it can be attacked by enemies or player to explode
		[Header("Common")]
		public ManualTimer countdown = new ManualTimer(2);
		public bool allowIgniteToCauseExplosion = true;


		private State state;
		private ExplosiveBarrelAvatar avatar;
		private StandardRecipient effects;
		private StandardHoldable holdable;
		private StandardThrowable throwable;
		private StandardRecipient systemic;

		// Start is called before the first frame update
		void Awake()
        {
			avatar = GetComponent<ExplosiveBarrelAvatar>();
			effects = GetComponent<StandardRecipient>();
			holdable = GetComponent<StandardHoldable>();
			throwable = GetComponent<StandardThrowable>();
			systemic = GetComponent<StandardRecipient>();


			state = State.Idle;

		}


		// Update is called once per frame
		void Update()
        {
			switch (state)
			{
				case State.Idle: UpdateIdle(); break;
				case State.Burning: UpdateBurning(); break;
				case State.Exploding: UpdateExploding(); break;
			}

		}

		private void UpdateIdle()
		{
			if (holdable.IsHeld && holdable.InputState.PrimaryDown)
			{
				throwable.ThrowObject();
				return;
			}

			bool ignited = effects.effects.Any(x => x.ignite);
			bool soaked = effects.effects.Any(x => x.soak);
			bool doused = effects.effects.Any(x => x.douse);
			if (ignited && !(soaked || doused))
			{
				countdown.Start();
				avatar.Activate();

				state = State.Burning;
			}
		}

		private void UpdateBurning()
		{
			countdown.Update(Time.deltaTime);

			if (holdable.IsHeld && holdable.InputState.PrimaryDown)
			{
				throwable.ThrowObject();
			}

			if (countdown.Done)
			{
				state = State.Exploding;
			}
		}

		private void UpdateExploding()
		{
			if (holdable.IsHeld)
			{
				holdable.Harness.Detach();
			}
			avatar.Explode();
		}


	}
}
