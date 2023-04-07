using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public class StandardSearchable : MonoBehaviour, ISearchable
	{
		public SearchState searchState;

		public SearchState GetSearchState()
		{
			return searchState;
		}
	}
}