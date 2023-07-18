// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.EndlessLevelGenerationSolution.PrefabHelper
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class Prefab : MonoBehaviour
    {
        public ConnectPoint.ConnectPoint leftEdge;
        public ConnectPoint.ConnectPoint rightEdge;
    }
}