// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.LevelGeneration.ConnectPoint
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class ConnectPoint : MonoBehaviour
    {
        public Edge edge;
    }
}