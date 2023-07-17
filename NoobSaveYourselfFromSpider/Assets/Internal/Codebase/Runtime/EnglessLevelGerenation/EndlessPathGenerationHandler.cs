// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.EnglessLevelGerenation
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class EndlessPathGenerationHandler : MonoBehaviour
    {
        public Transform root;
        public Transform initialPrefab;
        public Transform spawnPoint;
        public GameObject[] prefabs;

        public float speed = -5;

        public float spawnOffset = 40f;
        public float despawnOffset = 20f;
        public int maxBlocks = 20;

        private readonly List<GameObject> pool = new List<GameObject>();
        private float maxDespawnPositionX;

        [SerializeField, ReadOnly] private float xPos;

        private void Start()
        {
            xPos = spawnPoint.position.x;

            var previousPrefab = Instantiate(initialPrefab, root);
            previousPrefab.transform.position = new Vector2(xPos, spawnPoint.position.y);
            pool.Add(previousPrefab.gameObject);

            var previousCollider = previousPrefab.GetComponent<BoxCollider2D>();

            int index = -1;
            foreach (var prefab in prefabs)
            {
                if (index == -1)
                {
                    index = 1;
                    var instance = Instantiate(prefab, root);
                    var position = new Vector2(xPos, spawnPoint.position.y);
                    pool.Add(instance);
                    var boxCollider = instance.GetComponent<BoxCollider2D>();
                    xPos += previousCollider.size.x;
                    position.x = xPos;
                    instance.transform.position = position;
                    previousPrefab = instance.transform;
                    previousCollider = boxCollider;
                }
                else
                {
                    var instance = Instantiate(prefab, root);
                    var position = new Vector2(xPos, spawnPoint.position.y);
                    pool.Add(instance);
                    var boxCollider = instance.GetComponent<BoxCollider2D>();
                    xPos += previousCollider.size.x + previousCollider.offset.x - boxCollider.offset.x;
                    position.x = xPos;
                    instance.transform.position = position;
                    previousPrefab = instance.transform;
                    previousCollider = boxCollider;
                }
            }

            maxDespawnPositionX = pool[0].transform.position.x - despawnOffset;
        }

        private void SpawnBlock()
        {
            var randomIndex = Random.Range(0, prefabs.Length);
            var prefab = prefabs[randomIndex];

            var lastBlock = pool[pool.Count - 1];
            var lastBoxCollider = lastBlock.GetComponent<BoxCollider2D>();
            var lastBlockOffset = lastBoxCollider.offset;
            var lastBlockSize = lastBoxCollider.size;

            var currentBoxCollider = prefab.GetComponent<BoxCollider2D>();
            var offsetDiff = lastBlockOffset.x - currentBoxCollider.offset.x;
            var positionX = lastBlock.transform.position.x + lastBlockSize.x + offsetDiff;
            var positionY = spawnPoint.position.y;

            if (offsetDiff < 0)
            {
                positionX -= currentBoxCollider.offset.x;
            }

            var position = new Vector2(positionX, positionY);

            var instance = Instantiate(prefab, position, Quaternion.identity, root);
            pool.Add(instance);

            var firstBlock = pool[0];
            var firstBoxCollider = firstBlock.GetComponent<BoxCollider2D>();
            maxDespawnPositionX = firstBlock.transform.position.x - firstBoxCollider.size.x;
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

                if (block.transform.position.x + despawnOffset < -spawnOffset)
                {
                    DespawnBlock(block);
                    break;
                }
            }
        }

        private void DespawnBlock(GameObject block)
        {
            Destroy(block);
            pool.Remove(block);
        }
    }
}