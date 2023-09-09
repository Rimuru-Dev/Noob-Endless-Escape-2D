// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using System.Linq;
using Internal.Codebase.Runtime.GameplayScene.Obstacles;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.LevelGeneration.PrefabHelper
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class Prefab : MonoBehaviour
    {
        public ConnectPoint.ConnectPoint leftEdge;
        public ConnectPoint.ConnectPoint rightEdge;

        public List<DeadlyObstacle> defaultDeadlyObstacle;
        public List<DeadlyObstacle> trapDeadlyObstacle;

        public List<RewardView> rewardViews;
        public float spawnChance = 0.15f;

        private void Start()
        {
            var minActiveObstacles = trapDeadlyObstacle.Count / 2;
            var maxActiveObstacles = trapDeadlyObstacle.Count;
            var activeObstaclesCount = Random.Range(minActiveObstacles, maxActiveObstacles + 1);

            var minActiveRewards = rewardViews.Count / 2;
            var maxActiveRewards = rewardViews.Count;
            var activeRewardsCount = Random.Range(minActiveRewards, maxActiveRewards + 1);

            EnableRandomObstacles();

            bool spawnRewards = ShouldSpawnRewards(); // Проверка, будут ли награды активированы

            if (spawnRewards)
            {
                EnableRandomRewards();
            }

            DisableRandomObstacles();

            bool ShouldSpawnRewards()
            {
                // Генерируем случайное число и сравниваем его с вероятностью спавна
                float randomNum = Random.value;

                return randomNum < spawnChance;
            }

            void EnableRandomObstacles()
            {
                for (var i = 0; i < activeObstaclesCount; i++)
                {
                    var randomIndex = Random.Range(0, trapDeadlyObstacle.Count);

                    if (trapDeadlyObstacle[randomIndex] == null) continue;
                    trapDeadlyObstacle[randomIndex].gameObject.SetActive(true);
                    trapDeadlyObstacle.RemoveAt(randomIndex);
                }
            }

            void EnableRandomRewards()
            {
                for (var i = 0; i < activeRewardsCount; i++)
                {
                    if (rewardViews.Count > 0)
                    {
                        var randomIndex = Random.Range(0, rewardViews.Count);

                        if (rewardViews[randomIndex] == null)
                            continue;

                        rewardViews[randomIndex].gameObject.SetActive(true);
                        rewardViews.RemoveAt(randomIndex);
                    }
                }
            }

            void DisableRandomObstacles()
            {
                foreach (var obstacle in trapDeadlyObstacle.Where(x => x != null))
                    obstacle.gameObject.SetActive(false);
            }
        }
    }
}