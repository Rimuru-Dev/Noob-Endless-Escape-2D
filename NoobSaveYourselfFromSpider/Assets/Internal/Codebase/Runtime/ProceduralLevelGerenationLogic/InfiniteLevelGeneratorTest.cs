// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class InfiniteLevelGeneratorTest : MonoBehaviour
    {
        public List<GameObject> levelPrefabs;
        public Transform playerTransform;
        public float despawnDistance = 30f;
        public float spawnDistance = 15f;

        private float nextSpawnPoint = 0f;
        private Queue<GameObject> spawnedLevels = new Queue<GameObject>();

        private void Start()
        {
            nextSpawnPoint = playerTransform.position.x + spawnDistance;
        }

        private void Update()
        {
            // Check if the last spawned level is far enough to despawn
            if (spawnedLevels.Count > 0 && spawnedLevels.Peek().transform.position.x < playerTransform.position.x - despawnDistance)
            {
                GameObject despawnedLevel = spawnedLevels.Dequeue();
                Destroy(despawnedLevel);
            }

            // Check if we need to spawn a new level
            if (playerTransform.position.x > nextSpawnPoint)
            {
                int randomIndex = Random.Range(0, levelPrefabs.Count);
                GameObject levelPrefab = levelPrefabs[randomIndex];

                GameObject newLevel = Instantiate(levelPrefab, transform);
                float levelWidth = GetLevelWidth(newLevel);

                newLevel.transform.position = new Vector3(nextSpawnPoint + levelWidth / 2f, 0f, 0f);
                nextSpawnPoint += levelWidth;

                spawnedLevels.Enqueue(newLevel);
            }
        }

        private float GetLevelWidth(GameObject levelPrefab)
        {
            BoxCollider2D collider = levelPrefab.GetComponent<BoxCollider2D>();
            return collider.bounds.size.x;
        }
    }
}