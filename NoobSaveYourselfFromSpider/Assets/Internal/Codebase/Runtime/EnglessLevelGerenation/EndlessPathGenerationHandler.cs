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

            var previousCollider = previousPrefab.GetComponent<PrefabHelper>();

            int index = -1;
            foreach (var prefab in prefabs)
            {
                if (index == -1)
                {
                    index = 1;
                    var instance = Instantiate(prefab, root);
                    var position = new Vector2(xPos, spawnPoint.position.y);
                    pool.Add(instance);
                    var boxCollider = instance.GetComponent<PrefabHelper>();
                    xPos += previousCollider.PrefabSize.x;
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
                    var boxCollider = instance.GetComponent<PrefabHelper>();
                    xPos += previousCollider.PrefabSize.x + previousCollider.PrefabOffset.x - boxCollider.PrefabOffset.x;
                    position.x = xPos;
                    instance.transform.position = position;
                    previousPrefab = instance.transform;
                    previousCollider = boxCollider;
                }
            }

            var firstBlock = pool[0];
            var firstBoxCollider = firstBlock.GetComponent<PrefabHelper>();
            maxDespawnPositionX = firstBlock.transform.position.x - firstBoxCollider.PrefabSize.x;
        }

        
        private void SpawnBlock()
        {
            var randomIndex = Random.Range(0, prefabs.Length);
            var prefab = prefabs[randomIndex];

            var lastBlock = pool[pool.Count - 1];
            var lastBoxCollider = lastBlock.GetComponent<PrefabHelper>();
            var lastBlockOffset = lastBoxCollider.PrefabOffset;
            var lastBlockSize = lastBoxCollider.PrefabSize;

            var currentBoxCollider = prefab.GetComponent<PrefabHelper>();
            var offsetDiff = Mathf.Max(lastBlockOffset.x - currentBoxCollider.PrefabOffset.x, 0f);
            var position = new Vector2(lastBlock.transform.position.x + lastBlockSize.x + offsetDiff, spawnPoint.position.y);

            var instance = Instantiate(prefab, root);
            instance.transform.position = position;
            pool.Add(instance);

            var firstBlock = pool[0];
            var firstBoxCollider = firstBlock.GetComponent<PrefabHelper>();
            maxDespawnPositionX = firstBlock.transform.position.x - firstBoxCollider.PrefabSize.x;
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

                if (block.transform.position.x< maxDespawnPositionX)
                {
                    DespawnBlock(block);
                    maxDespawnPositionX = pool[0].transform.position.x - despawnOffset;
                    break;
                }
                // if (block.transform.position.x + despawnOffset < -spawnOffset)
                // {
                //     DespawnBlock(block);
                //     break;
                // }
            }
        }

        private void DespawnBlock(GameObject block)
        {
            Destroy(block);
            pool.Remove(block);
        }
    }
}