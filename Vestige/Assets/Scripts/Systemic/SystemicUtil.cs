using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Vestige
{
	public static class SystemicUtil
	{
		public static IEnumerable<IRecipient> GetRecipients(IEnumerable<Collider> hitTargets)
		{
			return hitTargets
				.Where(x => x.attachedRigidbody != null)
				.Select(x => x.attachedRigidbody.GetComponent<IRecipient>())
				.Where(x => x != null);
		}

		public static void BroadcastAsSelf(SystemicEffectTemplate template, GameObject self, IEnumerable<IRecipient> targets)
		{
			foreach (IRecipient r in targets)
			{
				r.RecieveEffect(template.AsEffect(self));
			}
		}

		public static void BroadcastIfExists(SystemicEffectTemplate template, GameObject self, IEnumerable<Collider> hitComponents)
		{
			BroadcastAsSelf(template, self, GetRecipients(hitComponents));
		}

		public static void BroadcastIfExists(SystemicEffectTemplate template, GameObject self, Collider hitComponent)
		{
			BroadcastIfExists(template, self, new Collider[] { hitComponent });
		}
	}
}