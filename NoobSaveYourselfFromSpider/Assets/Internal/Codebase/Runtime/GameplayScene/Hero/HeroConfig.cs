// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Cinemachine;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero
{
    [CreateAssetMenu(menuName = "Configs/Create " + nameof(HeroConfig), fileName = nameof(HeroConfig), order = 0)]
    public sealed class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public HeroViewController HeroViewControllerPrefab { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera HeroVirtualCamera { get; private set; }
    }
}