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
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.PrefabHelper;
using Internal.Codebase.Runtime.General.StorageData;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Handlers
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeNullComparison")]
    public sealed class EndlessLevelGenerationHandler : MonoBehaviour, IEndlessLevelGenerationHandler
    {
        [SerializeField, Expandable] private EndlessLevelGenerationConfig config;
        [SerializeField] private bool useStart = false;

        private List<Prefab> pool;
        private Prefab lastSpawnedPrefab;
        private Storage storage;
        private IYandexSaveService saveService;
        private IActionUpdaterService updater;
        private bool CanSpawnNextPrefab { get; set; } = true;
        public bool Pause { get; set; }

        public void Constructor(
            IYandexSaveService yandexSaveService,
            IActionUpdaterService actionUpdaterService,
            EndlessLevelGenerationConfig endlessLevelGenerationConfig)
        {
            saveService = yandexSaveService ?? throw new ArgumentNullException(nameof(yandexSaveService));
            updater = actionUpdaterService ?? throw new ArgumentNullException(nameof(actionUpdaterService));
            config = endlessLevelGenerationConfig
                ? endlessLevelGenerationConfig
                : throw new ArgumentNullException(nameof(endlessLevelGenerationConfig));
        }

        public void Perform()
        {
            updater.Subscribe(MyUdate, UpdateType.Update);
        }

        private void MyUdate()
        {
            print("MyUdate()");
        }

        private void Start()
        {
            if (useStart)
                Prepare();
        }

        // TODO: Start in Infrastructure. And Remove MonoBehaviour or Spawn This Object on scene in LevelFactory.
        public void Prepare()
        {
            pool = new List<Prefab>(config.MaxBlockCount);

            lastSpawnedPrefab = Instantiate(config.LaunchingPlatform, config.StartSpawnPoint, Quaternion.identity,
                transform);

            foreach (var reward in lastSpawnedPrefab.rewardViews.Where(reward => reward != null))
            {
                reward.Constructor(saveService);
                reward.Prepare();
            }

            pool.Add(lastSpawnedPrefab);

            StartEndlessLevelGeneration();
        }

        private void Update()
        {
            print("Unity Update()");

            if (Input.GetKeyDown(KeyCode.K)) updater.Pause(true);
            if (Input.GetKeyDown(KeyCode.L)) updater.Pause(false);

            if (Pause)
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
                var targetPosition = position + Vector3.left * (config.LevelScrollingSpeed * Time.deltaTime);

                block.transform.Translate(targetPosition - position);

                if (position.x < -config.DespawnOffset)
                    DespawnBlock(block);
            }
        }

        private void OnDestroy()
        {
            updater.Unsubscribe(MyUdate, UpdateType.Update);
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
                if (Pause)
                    yield return new WaitForSeconds(config.SpawnCooldown);

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

                    foreach (var reward in nextPrefab.rewardViews.Where(reward => reward != null))
                    {
                        reward.Constructor(saveService);
                        reward.Prepare();
                    }
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