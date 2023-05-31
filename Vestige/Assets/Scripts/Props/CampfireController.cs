using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige
{
	public class CampfireController : MonoBehaviour
	{
		public enum State
		{
			Idle, Activated
		}

		private State state;
		private StandardRecipient systemic;
		private StandardSearchable locator;
		private CampfireAvatar avatar;
		private PlayerStart playerStart;

		private void Awake()
		{
			systemic = GetComponent<StandardRecipient>();
			avatar = GetComponent<CampfireAvatar>();
			locator = GetComponent<StandardSearchable>();
			playerStart = GetComponentInChildren<PlayerStart>();

			state = State.Idle;
		}

		private void Update()
		{
			switch (state)
			{
				case State.Idle:
					if (systemic.effects.Any(x => x.ignite))
					{
						avatar.Ignite();
						locator.searchState.isFireSource = true;
						FindObjectOfType<GameSession>().currentPlayerStart = playerStart;
						state = State.Activated;
					}
					break;

				case State.Activated:
					break;
			}
		}
	}
}