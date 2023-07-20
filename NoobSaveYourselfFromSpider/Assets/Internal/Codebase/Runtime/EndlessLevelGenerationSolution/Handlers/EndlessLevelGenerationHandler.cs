// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Configs;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.PrefabHelper;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Handlers
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed class EndlessLevelGenerationHandler : MonoBehaviour, IEndlessLevelGenerationHandler
    {
        [SerializeField] private EndlessLevelGenerationConfig config;

        private List<Prefab> pool;
        private Prefab lastSpawnedPrefab;
        private bool CanSpawnNextPrefab { get; set; } = true;

        public void Constructor(EndlessLevelGenerationConfig endlessLevelGenerationConfig)
        {
            if (endlessLevelGenerationConfig == null)
                throw new ArgumentNullException(nameof(endlessLevelGenerationConfig));

            config = endlessLevelGenerationConfig;
        }

        // TODO: Start in Infrastructure. And Remove MonoBehaviour or Spawn This Object on scene in LevelFactory.
        public void Prepare()
        {
            pool = new List<Prefab>(config.MaxBlockCount);

            lastSpawnedPrefab = Instantiate(config.LaunchingPlatform, config.StartSpawnPoint, Quaternion.identity,
                transform);

            pool.Add(lastSpawnedPrefab);

            StartEndlessLevelGeneration();
        }

        private void Update()
        {
            if (IsCanLevelScrolling())
                return;

            for (var i = pool.Count - 1; i >= 0; i--)
            {
                var block = pool[i];
                if (block == null)
                {
                    pool.RemoveAt(i);
                    continue;
                }

                var position = block.transform.position;
                var targetPosition = position + Vector3.left * (config.LevelScrollingSpeed * Time.deltaTime);

                block.transform.Translate(targetPosition - position);

                if (position.x < -config.DespawnOffset)
                    DespawnBlock(block);
            }
        }

        private void OnDestroy()
        {
            StopEndlessLevelGeneration();

            pool.Clear();
            pool = null;
        }

        public void StartEndlessLevelGeneration()
        {
            CanSpawnNextPrefab = true;
            StartCoroutine(SpawnNextPrefab());
        }

        public void StopEndlessLevelGeneration()
        {
            CanSpawnNextPrefab = false;
            StopCoroutine(SpawnNextPrefab());
        }

        private IEnumerator SpawnNextPrefab()
        {
            while (CanSpawnNextPrefab)
            {
                var rightConnectPoint = lastSpawnedPrefab.rightEdge.transform;
                var nextPrefabIndex = Random.Range(0, config.Prefabs.Length);

                if (pool.Count < config.MaxBlockCount)
                {
                    var position = rightConnectPoint.position;
                    var nextPrefab = Instantiate(config.Prefabs[nextPrefabIndex], position, Quaternion.identity,
                        transform);

                    pool.Add(nextPrefab);

                    var leftConnectPoint = nextPrefab.leftEdge.transform;
                    var offset = leftConnectPoint.position - position;

                    nextPrefab.transform.position -= offset;

                    lastSpawnedPrefab = nextPrefab;
                }

                yield return new WaitForSeconds(config.SpawnCooldown);
            }
        }

        private bool IsCanLevelScrolling() =>
            pool == null || pool.Count == 0 || !CanSpawnNextPrefab;

        private void DespawnBlock(Prefab block)
        {
            if (pool.Contains(block))
                pool.Remove(block);

            if (block != null)
                Destroy(block.gameObject);
        }
    }
}