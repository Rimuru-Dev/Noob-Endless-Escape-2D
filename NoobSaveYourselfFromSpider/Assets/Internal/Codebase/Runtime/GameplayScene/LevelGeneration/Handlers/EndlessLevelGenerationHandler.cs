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
using System.Linq;
using Internal.Codebase.Infrastructure.Services.ActionUpdater;
using Internal.Codebase.Infrastructure.Services.CloudSave;
using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.PrefabHelper;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Handlers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeNullComparison")]
    public sealed class EndlessLevelGenerationHandler : IEndlessLevelGenerationHandler, IDisposable
    {
        private readonly ICoroutineRunner coroutineRunner;
        private readonly IYandexSaveService saveService;
        private readonly IActionUpdaterService updaterService;
        private readonly EndlessLevelGenerationConfig levelGeneration;
        private readonly Transform Root = new GameObject("Root").transform;

        private List<Prefab> pool;
        private Prefab lastSpawnedPrefab;
        private bool canSpawnNextPrefab;
        private bool pause;

        public EndlessLevelGenerationHandler(
            ICoroutineRunner coroutineRunner,
            IYandexSaveService saveService,
            IActionUpdaterService updaterService,
            EndlessLevelGenerationConfig levelGeneration)
        {
            this.coroutineRunner = coroutineRunner;
            this.saveService = saveService;
            this.updaterService = updaterService;
            this.levelGeneration = levelGeneration;
        }

        public void Prepare()
        {
            pool = new List<Prefab>(levelGeneration.MaxBlockCount);

            lastSpawnedPrefab =
                Object.Instantiate(
                    levelGeneration.LaunchingPlatform,
                    levelGeneration.StartSpawnPoint,
                    Quaternion.identity,
                    Root);

            foreach (var reward in lastSpawnedPrefab.rewardViews.Where(reward => reward != null))
            {
                reward.Constructor(saveService);
                reward.Prepare();
            }

            pool.Add(lastSpawnedPrefab);

            updaterService.Subscribe(Update, UpdateType.Update);
        }

        private void Update()
        {
            if (pause)
                return;

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
                var targetPosition = position + Vector3.left * (levelGeneration.LevelScrollingSpeed * Time.deltaTime);

                block.transform.Translate(targetPosition - position);

                if (position.x < -levelGeneration.DespawnOffset)
                    DespawnBlock(block);
            }
        }

        public void StartEndlessLevelGeneration()
        {
            pause = false;
            canSpawnNextPrefab = true;
            coroutineRunner.StartCoroutine(SpawnNextPrefab());
        }

        public void StopEndlessLevelGeneration()
        {
            pause = true;
            canSpawnNextPrefab = false;
            coroutineRunner.StopAllCoroutines();
        }

        public void Dispose()
        {
            StopEndlessLevelGeneration();
            pool.Clear();
            updaterService.Unsubscribe(Update, UpdateType.Update);
        }

        private IEnumerator SpawnNextPrefab()
        {
            while (canSpawnNextPrefab)
            {
                if (pause)
                    yield return new WaitForSeconds(levelGeneration.SpawnCooldown);

                try
                {
                    var rightConnectPoint = lastSpawnedPrefab.rightEdge.transform;
                    var nextPrefabIndex = Random.Range(0, levelGeneration.Prefabs.Length);

                    if (pool.Count < levelGeneration.MaxBlockCount)
                    {
                        var position = rightConnectPoint.position;
                        var nextPrefab = Object.Instantiate(levelGeneration.Prefabs[nextPrefabIndex], position,
                            Quaternion.identity,
                            Root);

                        pool.Add(nextPrefab);

                        var leftConnectPoint = nextPrefab.leftEdge.transform;
                        var offset = leftConnectPoint.position - position;

                        nextPrefab.transform.position -= offset;

                        lastSpawnedPrefab = nextPrefab;

                        foreach (var reward in nextPrefab.rewardViews.Where(reward => reward != null))
                        {
                            reward.Constructor(saveService);
                            reward.Prepare();
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    Debug.LogException(ex);
                    StopEndlessLevelGeneration();
                }
                catch (MissingReferenceException missingReference)
                {
                    const string message = "Incorrect termination of the world generator...";

                    Debug.Log($"<color=gray>{message} Source: {missingReference.Source}</color>");

                    Dispose();
                }

                yield return new WaitForSeconds(levelGeneration.SpawnCooldown);
            }
        }

        private bool IsCanLevelScrolling() =>
            pool == null || pool.Count == 0 || !canSpawnNextPrefab;

        private void DespawnBlock(Prefab block)
        {
            if (pool.Contains(block))
                pool.Remove(block);

            if (block != null)
                Object.Destroy(block.gameObject);
        }
    }
}