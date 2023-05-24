using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige
{
	public class CampfireController : MonoBehaviour
	{
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
		}

		private void Update()
		{
			bool shouldIgnite = systemic.effects.Any(x => x.ignite);
			if (shouldIgnite)
			{
				avatar.Ignite();
				locator.searchState.isFireSource = true;
				FindObjectOfType<GameSession>().currentPlayerStart = playerStart;
			}
		}
	}
}