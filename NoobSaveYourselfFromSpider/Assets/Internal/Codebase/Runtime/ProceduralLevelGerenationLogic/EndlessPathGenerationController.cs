// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class EndlessPathGenerationController : MonoBehaviour
    {
        public Transform root;
        public Transform spawnPoint;
        public GameObject[] prefabs;
        public float speed = -5;
        public float spawnOffset = 40f;
        public float despawnOffset = 20f;
        public int maxBlocks = 20;

        private List<GameObject> pool = new();
        private float spawnDistance;
        private float maxDespawnPositionX;

        private void Start()
        {
            float xPos = spawnPoint.position.x;
            foreach (var prefab in prefabs)
            {
                var instance = Instantiate(prefab, root);
                var position = new Vector2(xPos, spawnPoint.position.y);

                instance.transform.position = position;
                pool.Add(instance);

                var collider = instance.GetComponent<BoxCollider2D>();
                xPos += collider.size.x;
            }

            spawnDistance = pool[pool.Count - 1].transform.position.x - spawnPoint.position.x + spawnOffset;
            maxDespawnPositionX = pool[0].transform.position.x - despawnOffset;
        }

        private void Update()
        {
            if (pool.Count < maxBlocks)
                SpawnBlock();

            for (int i = pool.Count - 1; i >= 0; i--)
            {
                GameObject block = pool[i];
                if (block != null)
                {
                    block.transform.position = new Vector2(block.transform.position.x + speed * Time.deltaTime,
                        block.transform.position.y);

                    if (block.transform.position.x < maxDespawnPositionX)
                    {
                        DespawnBlock(block);
                        break;
                    }
                }
            }
        }

        private void SpawnBlock()
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            var prefab = prefabs[randomIndex];

            var lastBlock = pool[pool.Count - 1];
            var collider = lastBlock.GetComponent<BoxCollider2D>();
            var position = new Vector2(lastBlock.transform.position.x + collider.size.x, spawnPoint.position.y);

            var instance = Instantiate(prefab, position, Quaternion.identity, root);
            pool.Add(instance);
        }

        private void DespawnBlock(GameObject block)
        {
            Destroy(block);
            pool.Remove(block);
        }
    }
}