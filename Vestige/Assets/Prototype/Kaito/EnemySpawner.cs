using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vestige.Prototype
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnLocations;
        [SerializeField] private float spawnRadius = 2.5f;
        [SerializeField] private int maxSpawnAmount;
        [SerializeField] private float spawnDelay;

        private WaitForSeconds spawnWait;
        private float enemyRadius;
        private EnemyManager enemyManager;
        private int maxRandomSpawnTries = 10;

        private int spawnLocationIndex = 0;
        private Camera cam;

        private void Awake()
		{
            spawnWait = new WaitForSeconds(spawnDelay);
            enemyRadius = enemyPrefab.GetComponent<CapsuleCollider>().radius;
            enemyManager = enemyPrefab.GetComponent<EnemyManager>();

            cam = Camera.main;
        }

		private void Start()
		{
            enemyManager.detectionRadius = 30f;
            enemyManager.chaseDistance = 30f;
		}

		private void OnValidate()
		{
            // Prevent user from inputting negative values in Inspector for maxSpawnAmount & spawnDelay
            maxSpawnAmount = Mathf.Clamp(maxSpawnAmount, 0, 999);
            spawnDelay = Mathf.Clamp(spawnDelay, 0f, float.MaxValue);
		}

		public void StartSpawningEnemy()
		{
            StartCoroutine(SpawnEnemies());
		}

        // Spawn a set number of enemies. Pick spawn locations from an array of spawn locations
        // as a round robin.
        private IEnumerator SpawnEnemies()
		{
            for (int i = 0; i < maxSpawnAmount; i++)
            {
                SpawnEnemy();
                yield return spawnWait;
                MoveToNextIndex();
            }
        }

        // Spawn an enemy so it doesn't overlap with an existing enemy.
        private void SpawnEnemy()
		{
            Vector3 location = ChooseSpawnLocation();

            // Give up on finding a non-overlapping position after a set amount of tries
            // (avoid going into an infinite loop if position always overlaps with an enemy).
            while (OverlapsWithEnemy(location) && maxRandomSpawnTries > 0)
			{
                location = RandomizeSpawnLocation(location);
                maxRandomSpawnTries--;
			}

            Instantiate(enemyPrefab, location, Quaternion.identity);
        }

        // Returns true if a sphere with its center at location and radius of enemyRadius
        // overlaps with the CapsuleCollider on an enemy.
        private bool OverlapsWithEnemy(Vector3 location)
		{
            Collider[] colliders = Physics.OverlapSphere(location, enemyRadius);

            if (colliders == null)
                return false;

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<EnemyManager>(out EnemyManager enemy))
                {
                    return true;
                }
            }

            return false;
        }

        // Returns a random position inside a circle with the given center and
        // a spawn radius specified in the Inspector.
        private Vector3 RandomizeSpawnLocation(Vector3 center)
		{
            Vector3 newLocation = center;
            newLocation.x = center.x + Random.Range(-1f, 1f) * spawnRadius;
            // does not set y position because it assumes enemies are spawning on a flat surface
            newLocation.z = center.z + Random.Range(-1f, 1f) * spawnRadius;

            return newLocation;
		}

        // Chooses a location from the array of spawn locations that is not visible in the camera's view.
        private Vector3 ChooseSpawnLocation()
		{
            // Keep track of first spawn location that's being checked,
            // so I can exit out of the while loop when all positions have been checked
            int startingIndex = spawnLocationIndex;

            // This if statement accounts for the case when the first position to check is visible,
            // since the while condition for the following while loop cannot catch it.
            if (PositionIsVisible(spawnLocations[spawnLocationIndex].position))
			{
                MoveToNextIndex();
			}
            // Else, I've immediately found position not visible, so return & exit early
			else
			{
                return spawnLocations[spawnLocationIndex].position;
            }

            while (PositionIsVisible(spawnLocations[spawnLocationIndex].position) && spawnLocationIndex != startingIndex)
			{
                MoveToNextIndex();
			}

            // If I've gotten out of the while loop, I've either finally found a position not visible from the camera,
            // or all positions were visible so I'm giving up and returning the position I started the while loop with.
            return spawnLocations[spawnLocationIndex].position;
		}

        private void MoveToNextIndex()
		{
            spawnLocationIndex++;
            if (spawnLocationIndex >= spawnLocations.Length)
                spawnLocationIndex = 0;
        }

        // Returns true if a world position is inside the camera frustum, aka is visible.
        // Does not account for whether there is something occluding the camera view.
        private bool PositionIsVisible(Vector3 worldPos)
		{
            Vector3 viewPos = cam.WorldToViewportPoint(worldPos);
            if (IsBetween01(viewPos.x) && IsBetween01(viewPos.y) && viewPos.z > 0)
			{
                return true;
			}

            return false;
		}

        private bool IsBetween01(float x)
		{
            return x >= 0 && x <= 1;
		}


        // Helper function to draw the spawnRadius's in the scene
		private void OnDrawGizmos()
		{
            Gizmos.color = Color.red;
			for (int i = 0; i < spawnLocations.Length; i++)
			{
                Gizmos.DrawWireSphere(spawnLocations[i].position, spawnRadius);
			}
		}
	}
}
