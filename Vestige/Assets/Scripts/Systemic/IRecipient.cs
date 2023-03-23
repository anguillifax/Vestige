using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige
{
	public interface IRecipient
	{
		void RecieveEffect(Effect effect);
	}
}