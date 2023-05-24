using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vestige
{
	public class GameSession : MonoBehaviour
	{
		// =========================================================
		// Variables
		// =========================================================

		[Header("Player")]
		public PlayerStart currentPlayerStart;
		public GameObject prefabPlayer;
		public float respawnDelay = 3;

		// =========================================================
		// Common
		// =========================================================

		private void Start()
		{
			SpawnPlayer();
		}

		// =========================================================
		// Respawn
		// =========================================================

		public void SpawnPlayer()
		{
			var gameObj = Instantiate(prefabPlayer, GetPlayerSpawnPosition(), Quaternion.identity);
			var controller = gameObj.GetComponent<PlayerController>();
			FindObjectOfType<PlayerCameraFocus>().player = controller;
			Debug.Log($"[GameSession] Spawned player at {gameObj.transform.position}", gameObj);
		}

		public Vector3 GetPlayerSpawnPosition()
		{
			if (currentPlayerStart != null)
			{
				return currentPlayerStart.transform.position;
			}

			var starts = FindObjectsOfType<PlayerStart>();
			if (starts.Length == 0)
			{
				Debug.LogWarning("No player starts in scene. Spawning player at (0,0,0).");
				return Vector3.zero;
			}

			foreach (var s in starts)
			{
				if (s.defaultStart)
				{
					return s.transform.position;
				}
			}

			return starts[0].transform.position;
		}

		public void RespawnAfterDelay()
		{
			Invoke(nameof(SpawnPlayer), respawnDelay);
		}
	}
}