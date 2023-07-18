// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.ConnectPoint;
using Internal.Codebase.Runtime.EndlessLevelGenerationSolution.PrefabHelper;
using UnityEditor;
using UnityEngine;

namespace Internal.Codebase.Runtime.EndlessLevelGenerationSolution.Editor
{
    [CustomEditor(typeof(Prefab))]
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeNullComparison")]
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
    }
}