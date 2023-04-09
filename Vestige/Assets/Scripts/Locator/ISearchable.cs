using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public interface ISearchable
	{
		SearchState GetSearchState();
	}
}