using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class EffectCache : IEnumerable<Effect>
	{
		private List<Effect> active = new List<Effect>();
		private List<Effect> next = new List<Effect>();

		public void Add(Effect effect)
		{
			next.Add(effect);
		}

		public void Flip()
		{
			active = next;
			next = new List<Effect>();
		}

		public IEnumerator<Effect> GetEnumerator() => ((IEnumerable<Effect>)active).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)active).GetEnumerator();
		public int Count => active.Count;
	}
}