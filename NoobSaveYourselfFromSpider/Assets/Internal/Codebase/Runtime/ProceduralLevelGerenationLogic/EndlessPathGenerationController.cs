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
        public Transform initialPrefab;
        public Transform spawnPoint;
        public GameObject[] prefabs;
        public float speed = -5;
        public float spawnOffset = 40f;
        public float despawnOffset = 20f;
        public int maxBlocks = 20;

        private readonly List<GameObject> pool = new();
        private float spawnDistance;
        private float maxDespawnPositionX;

        private void Start()
        {
            var xPos = spawnPoint.position.x;

            {
                var instance = Instantiate(initialPrefab, root);
                var position = new Vector2(xPos, spawnPoint.position.y);

                instance.transform.position = position;
                pool.Add(instance.gameObject);

                var boxCollider2D = instance.GetComponent<BoxCollider2D>();
                xPos += boxCollider2D.size.x;
            }

            foreach (var prefab in prefabs)
            {
                var instance = Instantiate(prefab, root);
                var position = new Vector2(xPos, spawnPoint.position.y);

                instance.transform.position = position;
                pool.Add(instance);

                var boxCollider2D = instance.GetComponent<BoxCollider2D>();
                xPos += boxCollider2D.size.x;
            }

            spawnDistance = pool[^1].transform.position.x - spawnPoint.position.x + spawnOffset;
            maxDespawnPositionX = pool[0].transform.position.x - despawnOffset;
        }

        private void Update()
        {
            if (pool.Count < maxBlocks)
                SpawnBlock();

            for (var i = pool.Count - 1; i >= 0; i--)
            {
                var block = pool[i];

                if (block == null)
                    continue;

                block.transform.position = new Vector2(block.transform.position.x + speed * Time.deltaTime,
                    block.transform.position.y);

                if (block.transform.position.x < maxDespawnPositionX)
                {
                    DespawnBlock(block);
                    break;
                }
            }
        }

        private void SpawnBlock()
        {
            var randomIndex = Random.Range(0, prefabs.Length);
            var prefab = prefabs[randomIndex];

            var lastBlock = pool[^1];
            var boxCollider2D = lastBlock.GetComponent<BoxCollider2D>();
            var position = new Vector2(lastBlock.transform.position.x + boxCollider2D.size.x, spawnPoint.position.y);

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