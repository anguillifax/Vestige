using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Vestige
{
	public static class MenuCommands
	{
		[MenuItem("Vestige/Make Systemic ^&y")]
		public static void MakeSystemic()
		{
			GameObject[] targets = Selection.GetFiltered<GameObject>(SelectionMode.Editable);

			Undo.SetCurrentGroupName($"Make Systemic ({targets.Length} Selected)");
			int group = Undo.GetCurrentGroup();

			Undo.RecordObjects(targets, "Edit components");
			foreach (GameObject go in targets)
			{
				if (!go.GetComponent<Rigidbody>())
				{
					var rb = Undo.AddComponent<Rigidbody>(go);
					rb.isKinematic = true;
				}

				if (!go.GetComponent<StandardRecipient>())
				{
					Undo.AddComponent<StandardRecipient>(go);
				}
			}

			Undo.CollapseUndoOperations(group);
		}
	}
}