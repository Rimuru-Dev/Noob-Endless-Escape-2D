// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.ConnectPoint;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.PrefabHelper;
using Internal.Codebase.Runtime.Obstacles;
using UnityEditor;
using UnityEngine;

namespace Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Editor
{
    [CustomEditor(typeof(Prefab))]
    public sealed class PrefabHelperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Load ConnectPoints"))
                FindConnectPoints();

            if (GUILayout.Button("Show Connection Point"))
                ShowHideSpriteRenderer(true);

            if (GUILayout.Button("Hide Connection Point"))
                ShowHideSpriteRenderer(false);

            // if (GUILayout.Button("CACHE ALL COLLIDERS!!!"))
            CacheColliders();

            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }

        private void ShowHideSpriteRenderer(bool isActive)
        {
            var prefabHelper = (Prefab)target;
            var connectPoints = prefabHelper.GetComponentsInChildren<ConnectPoint.ConnectPoint>(true);

            foreach (var connectPoint in connectPoints)
            {
                connectPoint.GetComponentsInChildren<SpriteRenderer>(true)
                    .Where(x => x != null)
                    .ToList()
                    .ForEach(x => { x.enabled = isActive; });
            }
        }

        private void FindConnectPoints()
        {
            var prefabHelper = (Prefab)target;
            var connectPoints = prefabHelper.GetComponentsInChildren<ConnectPoint.ConnectPoint>(true);

            foreach (var connectPoint in connectPoints.Where(x => x != null))
            {
                switch (connectPoint.edge)
                {
                    case Edge.Left:
                        prefabHelper.leftEdge = connectPoint;
                        break;
                    case Edge.Right:
                        prefabHelper.rightEdge = connectPoint;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void CacheColliders()
        {
            var prefabHelper = (Prefab)target;
            var deadlyObstacles = prefabHelper.GetComponentsInChildren<DeadlyObstacle>(true);

            prefabHelper.defaultDeadlyObstacle = new List<DeadlyObstacle>();
            prefabHelper.trapDeadlyObstacle = new List<DeadlyObstacle>();

            foreach (var deadlyObstacle in deadlyObstacles.Where(x => x != null))
            {
                switch (deadlyObstacle.DeadlyObstacleTypeID)
                {
                    case DeadlyObstacleTypeID.Default:
                        prefabHelper.defaultDeadlyObstacle.Add(deadlyObstacle);
                        break;
                    case DeadlyObstacleTypeID.Trap:
                        prefabHelper.trapDeadlyObstacle.Add(deadlyObstacle);
                        break;
                    default:
                        prefabHelper.defaultDeadlyObstacle.Add(deadlyObstacle);
                        break;
                }
            }
        }
    }
}