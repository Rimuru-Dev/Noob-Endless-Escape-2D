// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Runtime.Obstacles;
using UnityEngine;

namespace Internal.Codebase.Runtime.EndlessLevelGenerationSolution.PrefabHelper
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class Prefab : MonoBehaviour
    {
        public ConnectPoint.ConnectPoint leftEdge;
        public ConnectPoint.ConnectPoint rightEdge;

        public List<DeadlyObstacle> defaultDeadlyObstacle;
        public List<DeadlyObstacle> trapDeadlyObstacle;

        // TODO: Transfer to  RandomActivationService
        private void Start()
        {
            var minActiveObstacles = trapDeadlyObstacle.Count / 2;
            var maxActiveObstacles = trapDeadlyObstacle.Count;
            var activeObstaclesCount = Random.Range(minActiveObstacles, maxActiveObstacles + 1);

            EnableRandomObstacles();

            DisableRandomObstacles();

            void EnableRandomObstacles()
            {
                for (var i = 0; i < activeObstaclesCount; i++)
                {
                    var randomIndex = Random.Range(0, trapDeadlyObstacle.Count);

                    trapDeadlyObstacle[randomIndex].gameObject.SetActive(true);
                    trapDeadlyObstacle.RemoveAt(randomIndex);
                }
            }

            void DisableRandomObstacles()
            {
                foreach (var obstacle in trapDeadlyObstacle)
                    obstacle.gameObject.SetActive(false);
            }
        }
    }
}