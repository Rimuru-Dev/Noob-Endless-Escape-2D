// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Hero
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class Hero : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer HeroSpriteRenderer { get; private set; }

        [SerializeField] private JumpController jumpController;
    }
}